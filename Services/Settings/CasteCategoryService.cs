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
    public class CasteCategoryService : IService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(CasteCategoryService));
        private readonly IConfiguration _configuration;

        private string r_code = nameof(RCode.Success);
        private string r_data = "";
        private object r_object = new object();
        private List<object> r_list = new List<object>();
        public CasteCategoryService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public object GetObject(JObject data)
        {
            log.Debug("GetCasteCategory process starts");

            try
            {
                object objMember = data["data"];
                JObject joMember = (JObject)objMember;
                JObject NewData = new JObject();
                object objSHA = data["SHA"];
                NewData["data"] = joMember;
                NewData["SHA"] = (JObject)objSHA;
                Common.ValidDatePostedData(NewData, "data");
                var dbObjEntity = data["data"].ToObject<MCasteCat>();
                if (dbObjEntity != null)
                {
                    int caseId = dbObjEntity.CatseCategoryId;
                    using (mararkContext dbcontext = AppSettingValue.GetConnectionObject(_configuration))
                    {
                        r_object = (from x in dbcontext.MCasteCat
                                    where x.CatseCategoryId == caseId && x.Status == 1
                                    select x).FirstOrDefault();

                    }
                }
                else
                {
                    r_object = "Caste Category Id not found";
                    log.Info("Caste Category Id not found");
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

            log.Debug("GetCasteCategory process ends");
            return (new { r_code, r_data = data, r_object });
        }

        public object GetObjectList(JObject R_PRM)
        {
            log.Debug("GetCasteCategoryList process starts");

            try
            {

                using (mararkContext dbcontext = AppSettingValue.GetConnectionObject(_configuration))
                {
                    r_list = (from x in dbcontext.MCasteCat
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
            log.Debug("GetCasteCategoryList process ends");
            return (new { r_code, r_data = R_PRM, r_list });
        }


        public object SaveOrUpdate(JObject data, string action)
        {
            log.Debug("SaveOrUpdate process starts");
            object NewToken = new object();

            List<object> _list = new List<object>();
            try
            {
                object objMember = data["data"];
                JObject joMember = (JObject)objMember;
                JObject NewData = new JObject();
                object objSHA = data["SHA"];
                NewData["data"] = joMember;
                NewData["SHA"] = (JObject)objSHA;
                Common.ValidDatePostedData(NewData, "data");
                var dbObjEntity = data["data"].ToObject<MCasteCat>();
                using (mararkContext dbcontext = AppSettingValue.GetConnectionObject(_configuration))
                {
                    using (var transaction = dbcontext.Database.BeginTransaction())
                    {
                        try
                        {
                            Common.UpdateLoginAttempts(dbcontext, data);

                            if (action == "SAVE")
                            {
                                dbcontext.MCasteCat.Add(dbObjEntity);
                                dbcontext.SaveChanges();
                                transaction.Commit();
                            }
                            if (action == "UPDATE")
                            {
                                dbcontext.Entry(dbObjEntity).State = EntityState.Modified;
                                dbcontext.MCasteCat.Update(dbObjEntity);
                                dbcontext.SaveChanges();
                                transaction.Commit();
                            }
                            if (action == "DELETE")
                            {
                                int _Id = dbObjEntity.CatseCategoryId;
                                var CasteCategory = (from x in dbcontext.MCasteCat
                                                where x.CatseCategoryId == _Id
                                                     select x).FirstOrDefault();
                                CasteCategory.Status = 0;
                                dbcontext.Entry(CasteCategory).State = EntityState.Modified;
                                dbcontext.MCasteCat.Update(CasteCategory);
                                dbcontext.SaveChanges();
                                transaction.Commit();
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

                NewToken = (Common.GetToken(data["SHA"]["clientid"].ToString(), _configuration));


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

            Newtonsoft.Json.Linq.JObject o = new Newtonsoft.Json.Linq.JObject
            {
                { "Save", true },

            };
            o.Merge((JObject)NewToken);
            log.Debug("SaveOrUpdate process ends");
            return (o, _list);
        }
    }
}
