using System;
using System.Collections.Generic;
using CorePlugs20.OdinSecurity;
using CorePlugs20.OdinString;

namespace Authen.Models
{
    public class SampleData
    {
        public static void Init(AuthenEntities blogEntities)
        {
            List<InvokerAuthen_DbModel> authens = new List<InvokerAuthen_DbModel>();


            // apiModel client pwd   bb78b70da51dbf37533378a759808a4c
            InvokerAuthen_DbModel apiModel = new InvokerAuthen_DbModel 
            { 
                ProjcetName="apiblog",
                Invoker="f12895548bf2492587ccfdec84fa7eab", //  Guid.NewGuid().ToString("N")
                Sale = "jAxNzA5MTIxOTQ1NDc=",               //  dt.ToString("yyyyMMddHHmmss").SubString(1)
                Key = "c83eca4d237a289b7438698b83622bc3"    //  (clientPwd+Sal).StringToMd5ToLower()
            };
            authens.Add(apiModel);


            // uiModel client pwd   c703368e3a6b9cd87046de51f917d471
            InvokerAuthen_DbModel uiModel = new InvokerAuthen_DbModel 
            { 
                ProjcetName="uiblog",
                Invoker="40577c827196456a87278b5c0d9a5b13", //  Guid.NewGuid().ToString("N")
                Sale = "jAxNzA5MTMwOTUzMTM=",               //  dt.ToString("yyyyMMddHHmmss").SubString(1)
                Key = "e25b83dcc56bc04087c726a055ef3b4e"    //  (clientPwd+Sal).StringToMd5ToLower()
            };
            authens.Add(uiModel);

            // authenModel client pwd   6bb3b0ad17ea7e9ce4565a2fe17280db
            InvokerAuthen_DbModel authenModel = new InvokerAuthen_DbModel 
            { 
                ProjcetName="authen",
                Invoker="99493a7f911e4e1aa5af957851fe8383", //  Guid.NewGuid().ToString("N")
                Sale = "jAxNzA5MTQxODIyMjE=",               //  dt.ToString("yyyyMMddHHmmss").SubString(1)
                Key = "c0563cfe53dd6ef2419d04d277bbc5dc"    //  (clientPwd+Sal).StringToMd5ToLower()
            };
            authens.Add(authenModel);



            blogEntities.InvokerAuthens.AddRange(authens);

            blogEntities.SaveChanges();
        }
    }
}