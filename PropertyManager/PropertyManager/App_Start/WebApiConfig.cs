using AutoMapper;
using PropertyManager.Domain;
using PropertyManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;


namespace PropertyManager
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application / xml"); config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            CreateMaps();
        }

        private static void CreateMaps()
        {
            Mapper.CreateMap<Address, AddressesModel>();
            Mapper.CreateMap<Lease, LeasesModel>();
            Mapper.CreateMap<Property, PropertiesModel>();
            Mapper.CreateMap<Tenant, TenantsModel>();
            Mapper.CreateMap<WorkOrder, WorkOrdersModel>();

        }
    }
}
