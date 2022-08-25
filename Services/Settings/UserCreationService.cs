using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MT.DBLayer;
using MT.Helper;
using MT.Services.Interface;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MT.Services.Settings
{
    public class UserCreationService : IService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(UserCreationService));
        private readonly IConfiguration _configuration;

        private string r_code = nameof(RCode.Success);
        private string r_data = "";
        private object r_object = new object();
        private List<object> r_list = new List<object>();

        public UserCreationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public object GetObject(JObject data)
        {
            log.Debug("GetUser process starts");
           
            try
            {
                object objMember = data["data"];
                JObject joMember = (JObject)objMember;
                JObject NewData = new JObject();
                object objSHA = data["SHA"];
                NewData["data"] = joMember;
                NewData["SHA"] = (JObject)objSHA;
                Common.ValidDatePostedData(NewData, "data");
                var dbObjEntity = data["data"].ToObject<MUsers>();
                if (dbObjEntity != null)
                {
                    long _UserId = dbObjEntity.UserId;
                    using (mararkContext dbcontext = AppSettingValue.GetConnectionObject(_configuration))
                    {
                        r_object = (from x in dbcontext.MUsers
                                    where x.UserId == _UserId && x.Status == 1
                                    select x).FirstOrDefault();

                    }
                }
                else
                {
                    r_object = "User Id not found";
                    log.Info("User Id not found");
                }
            }
            catch (MySqlException sqlEx)
            {
                log.Error(sqlEx);
                throw;
            }
            catch (Exception s)
            {
                log.Error(s);
                r_code = s.Message;

            }

            log.Debug("GetUser process ends");
            return (new { r_code, r_data = data, r_object });
        }

        public object GetObjectList(JObject R_PRM)
        {
            log.Debug("GetUser process starts");
           
            try
            {

                using (mararkContext dbcontext = AppSettingValue.GetConnectionObject(_configuration))
                {
                    r_list = (from x in dbcontext.MUsers
                              where x.Status == 1
                             select x).ToList<object>();

                }
            }
            catch (MySqlException sqlEx)
            {
                log.Error(sqlEx);
                throw;
            }
            catch (Exception s)
            {
                log.Error(s);
                r_code = s.Message;

            }
            log.Debug("GetUser process ends");
            return (new { r_code, r_data = R_PRM, r_list });
        }


        public object SaveOrUpdate(JObject data,string action)
        {
            log.Debug("SaveOrUpdate process starts");
            object NewToken = new object();
            
            List<object> _list = new List<object>();
            try
            {
                NewToken = (Common.GetToken(data["SHA"]["clientid"].ToString(), _configuration));
                object objMember = data["data"];
                JObject joMember = (JObject)objMember;
                JObject NewData = new JObject();
                object objSHA = data["SHA"];
                NewData["data"] = joMember;
                NewData["SHA"] = (JObject)objSHA;
                Common.ValidDatePostedData(NewData, "data");
                var dbObjEntity = data["data"].ToObject<MUsers>();
                var dbLoginEntity = new Logins();
                var r_message = "";
                using (mararkContext dbcontext = AppSettingValue.GetConnectionObject(_configuration))
                {
                    using (var transaction = dbcontext.Database.BeginTransaction())
                    {
                        try
                        {                            
                            Common.UpdateLoginAttempts(dbcontext, data);

                            if (action == "SAVE")
                            {

                                var login = (from x in dbcontext.Logins
                                             where x.UserId.ToLower().Trim() == dbObjEntity.UserName.ToLower().Trim()
                                             select x).FirstOrDefault();
                                if(login != null && !String.IsNullOrEmpty(login.UserId))
                                {
                                    transaction.Commit();
                                    r_message = "Username already exists";                                     
                                    return (new { r_message, r_data = data });

                                }

                                dbObjEntity.LastStatusAt = DateTime.Now.ToLocalTime();
                                dbcontext.MUsers.Add(dbObjEntity);

                                dbLoginEntity.UserId = dbObjEntity.UserName.ToLower();
                                dbLoginEntity.Name = dbObjEntity.UserName.ToUpper();
                                dbLoginEntity.Password = dbObjEntity.UserPassword;
                                dbLoginEntity.Utype = 2001;
                                dbLoginEntity.IsActive = 1;
                                dbLoginEntity.DeviceId = "1";

                                dbcontext.Logins.Add(dbLoginEntity);

                                dbcontext.SaveChanges();
                                transaction.Commit();

                                r_message = "Record saved successfully";
                                return (new { r_message, r_data = data });
                            }
                            if (action == "UPDATE")
                            {
                                var login = (from x in dbcontext.Logins
                                             where x.UserId.ToLower().Trim() == dbObjEntity.UserName.ToLower().Trim()
                                             select x).FirstOrDefault();
                                if (login != null && !String.IsNullOrEmpty(login.UserId))
                                {
                                    transaction.Commit();
                                    r_message = "Username already exists";
                                    return (new { r_message, r_data = data });

                                }

                                dbObjEntity.LastStatusAt = DateTime.Now.ToLocalTime();
                                dbcontext.Entry(dbObjEntity).State = EntityState.Modified;
                                dbcontext.MUsers.Update(dbObjEntity);
                              
                               
                                dbcontext.SaveChanges();
                                transaction.Commit();
                                r_message = "Record updated successfully";
                                return (new { r_message, r_data = data });
                            }
                            if (action == "DELETE")
                            {
                                long _UserId = dbObjEntity.UserId;
                                var user = (from x in dbcontext.MUsers
                                            where x.UserId == _UserId
                                            select x).FirstOrDefault();
                                user.Status = 0;
                                user.LastStatusAt = DateTime.Now.ToLocalTime();
                                dbcontext.Entry(user).State = EntityState.Modified;
                                dbcontext.MUsers.Update(user);

                                var login = (from x in dbcontext.Logins
                                            where x.UserId == user.UserName
                                            select x).FirstOrDefault();
                                dbLoginEntity.IsActive = 0;
                                dbcontext.Entry(login).State = EntityState.Deleted;
                                //dbcontext.Logins.Update(login);
                                

                                dbcontext.SaveChanges();
                                transaction.Commit();
                                r_message = "Record deleted successfully";
                                return (new { r_message, r_data = data });
                            }
                        }
                        catch (MySqlException sqlEx)
                        {
                            log.Error(sqlEx);
                            throw;
                        }
                        catch (Exception ex)
                        {
                            log.Error(ex);
                            throw;
                        }


                    }

                }
            }
            catch(MySqlException sqlEx)
            {
                log.Error(sqlEx);
                throw;
            }
            catch(Exception ex) 
            {
                log.Error(ex);
                throw; 
            }
            finally
            {
                log.Debug("SaveOrUpdate process ends");
            }
            Newtonsoft.Json.Linq.JObject o = new Newtonsoft.Json.Linq.JObject
            {
                { "Save", true },

            };
            o.Merge((JObject)NewToken);
           
            return (o, _list);
        }
    }
}
