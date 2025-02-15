﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReflectIt.Tests
{
    [TestClass]
    public class IoCTests
    {
        [TestMethod]
        public void Can_Resolve_Types()
        {
            var ioc = new Container();
            ioc.For<ILogger>().Use<SqlServerLogger>(); //Container configuration

            var logger = ioc.Resolve<ILogger>();

            Assert.AreEqual(typeof(SqlServerLogger), logger.GetType());
        }

        [TestMethod]
        public void Can_Resolve_Types_Without_Default_Ctor()
        {
            var ioc = new Container();
            ioc.For<ILogger>().Use<SqlServerLogger>();
            ioc.For<IRepository<Employee>>().Use<SqlRepository<Employee>>();

            var repository = ioc.Resolve<IRepository<Employee>>();

            Assert.AreEqual(typeof(SqlRepository<Employee>), repository.GetType());
        }

        [TestMethod]
        public void Can_Resolve_Concrete_Type()
        {
            var ioc = new Container();
            ioc.For<ILogger>().Use<SqlServerLogger>();
            ioc.For(typeof(IRepository<>)).Use(typeof(SqlRepository<>)); //IRepository<> of anything (unbound generic)

            var service = ioc.Resolve<InvoiceService>();

            Assert.IsNotNull(service);
        }
    }
}