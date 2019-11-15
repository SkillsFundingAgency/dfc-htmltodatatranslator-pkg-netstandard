using AutoMapper;
using DFC.HtmlToDataTranslator.Services;
using DFC.HtmlToDataTranslator.TypeConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DFC.HtmlToDataTranslator.UnitTests.AutoMapperTests
{
    public class DITests
    {
        [Fact]
        public void CanInject()
        {
            //Arrange
            var htmlDataTranslator = new HtmlAgilityPackDataTranslator();
            var htmltoStringTypeConverter = new HtmlToStringTypeConverter(htmlDataTranslator);

            var config = new MapperConfiguration(options =>
            {
                options.CreateMap<string, List<string>>().ConvertUsing(htmltoStringTypeConverter);
                options.CreateMap<DataModel, ViewModel>();
            });

            var mapper = config.CreateMapper();
            var markup = @"<p>p1<a href=""http://www.google.com"">Google</a></p>";
            var dataModel = new DataModel() { Id = Guid.NewGuid(), Markup = markup };

            //Act
            var viewModel = mapper.Map<ViewModel>(dataModel);

            //Asset
            Assert.Single(viewModel.Markup);
            Assert.Equal("p1[Google | http://www.google.com]", viewModel.Markup.First());
        }
    }
}
