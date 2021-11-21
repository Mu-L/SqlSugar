﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrmTest
{
    public partial class NewUnitTest
    {
        public static void Bulk()
        {
            Db.CodeFirst.InitTables<UnitIdentity1>();
            Db.DbMaintenance.TruncateTable<UnitIdentity1>();
           var data1 = new UnitIdentity1()
            {
                Name = "a",
                Id=1
            };
            var data2 = new UnitIdentity1()
            {
                Name = "b",
                Id = 2
            };
            var data3 = new UnitIdentity1()
            {
                Name = "c",
                Id = 3
            };
            Db.Fastest<UnitIdentity1>().BulkCopy(new List<UnitIdentity1>() {
              data1
            });
            var list=Db.Queryable<UnitIdentity1>().ToList();
            if (list.Count != 1 || data1.Name != list.First().Name) 
            {
                throw new Exception("unit Bulk");
            }
   
            Db.Fastest<UnitIdentity1>().BulkCopy(new List<UnitIdentity1>() {
              data2,
              data3
            });
            list = Db.Queryable<UnitIdentity1>().ToList();
            if (list.Count != 3 || !list.Any(it=>it.Name=="c"))
            {
                throw new Exception("unit Bulk");
            }
            Db.Fastest<UnitIdentity1>().BulkUpdate(new List<UnitIdentity1>() {
               new UnitIdentity1(){
                Id=1,
                 Name="222"
               },
                 new UnitIdentity1(){
                Id=2,
                 Name="111"
               }
            });
            list = Db.Queryable<UnitIdentity1>().ToList();
            if (list.First(it=>it.Id==1).Name!="222")
            {
                throw new Exception("unit Bulk");
            }
            if (list.First(it => it.Id == 2).Name != "111")
            {
                throw new Exception("unit Bulk");
            }
            if (list.First(it => it.Id == 3).Name != "c")
            {
                throw new Exception("unit Bulk");
            }
        }
    }
    public class UnitIdentity1 
    {
        [SqlSugar.SugarColumn(IsPrimaryKey =true)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}