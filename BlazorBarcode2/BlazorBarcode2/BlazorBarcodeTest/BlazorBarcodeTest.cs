using AngleSharp.Dom;
using AngleSharp.Svg.Dom;
using Bunit;
using Bunit.Mocking.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Syncfusion.Blazor;
using Syncfusion.Blazor.BarcodeGenerator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace BlazorBarcodeTest
{
    public class BlazorBarcodeImplicitContextTest : Bunit.TestContext
    {
        private Bunit.TestContext Context;
        MockJsRuntimeInvokeHandler mockJsRt;
        JsRuntimePlannedInvocation<bool> renderInvocation;
        [SetUp]
        public void Setup()
        {
            using (Context = new Bunit.TestContext()) ;

            //Context.Services.AddSingleton<BlazorBarcode2.SyncfusionService>(new BlazorBarcode2.SyncfusionService());
           // Context.Services.AddMockJsRuntime();
            Context.Services.AddSyncfusionBlazor();

            mockJsRt = Context.Services.AddMockJsRuntime();
            renderInvocation = mockJsRt.Setup<bool>("render");
            renderInvocation.SetResult(true);
        }
        [Inject]
        SyncfusionBlazorService SyncfusionBlazorService { get; set; }
        [Test]
        public void BarcodeComponentTest()
        {
            // Arrange

            string expectedMarkup = "<h3>BlazorBarcode</h3><button class='render'>Click Me</button>";

            // Act

            var cut = Context.RenderComponent<BlazorBarcode2.BlazorBarcode>();

            // Assert
            cut.MarkupMatches(expectedMarkup);
        }

        [Test]
        public async Task Code39RenderTest()
        {
            //var textService = new TaskCompletionSource<bool>();
            var cut = Context.RenderComponent<BlazorBarcode2.Pages.Index>();// parameters => parameters.Add(p => p.TextService, textService.Task));
            //textService.SetResult(true);
            await Task.Delay(500);
            cut.WaitForAssertion(() =>  StringAssert.Contains("text", cut.Markup), TimeSpan.FromSeconds(20));
            var renderedMarkup = cut.Markup;
            Console.WriteLine(renderedMarkup);
        }
    }
}
