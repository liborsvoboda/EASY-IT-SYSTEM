﻿using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using Newtonsoft.Json;
using System.Linq;
using System.Windows.Controls;

namespace EasyITSystemCenter.Pages {

    public partial class HtmlFullEditorPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static TemplateClassList selectedRecord = new TemplateClassList();

        public HtmlFullEditorPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            HtmlEditor.HtmlContent = "<HTML><HEAD></HEAD>\r\n<BODY>\r\n<H3><IMG title=remote-support-services-500x500 style=\"HEIGHT: 39px; WIDTH: 68px\" alt=remote-support-services-500x500 src=\"data:image/png;base64,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\" width=75 height=48></H3>\r\n<H3>What is EASY-SYSTEM-Builder? </H3>\r\n<P style=\"FONT-SIZE: 13px; MARGIN-TOP: 0px\">EASY-DATA-Center [ESB] is Visual Studio Project for immediately Start Extremely Fast Development of ANY SYSTEM.</P>\r\n<P style=\"FONT-SIZE: 13px; MARGIN-TOP: 0px\">ESB is universal SYSTEM TEmplate for Next Developing only by FORM Builder early by Graphics Designer only. </P>\r\n<P style=\"FONT-SIZE: 18px\">ESB Immediately Prepared for: ANY MS SYSTEMS [web Planned] &amp; Applications using as:</P>\r\n<P style=\"FONT-SIZE: 20px; MARGIN-TOP: 0px\">IS Systems | Touch Systems |Terminal Panels |Controling-DWHS Systems</P>\r\n<P style=\"FONT-SIZE: 20px; MARGIN-TOP: 0px\">MultiMedia Systems | Machine Control Panels | Production Terminals | Etc..</P>\r\n<P style=\"FONT-SIZE: 20px\">Based on 3 Layer Technology. </P>\r\n<P style=\"FONT-SIZE: 20px\">For next 1 year is Planned full compatible WEB client on METRO4 (this web)</P>\r\n<H5 style=\"MARGIN-BOTTOM: 0px\">2 of 50 used Technologies open Unlimited Possibilities:</H5>\r\n<P style=\"FONT-SIZE: 20px; MARGIN-TOP: 0px\"><A title=\"Click for Info About First Technology\" href=\"https://mahapps.com/\" target=_blank>Design Technology Metro</A></P>\r\n<P style=\"FONT-SIZE: 20px; MARGIN-TOP: 0px\"><A title=\"Click for Open Support DB List\" href=\"https://learn.microsoft.com/en-us/dotnet/desktop/wpf/overview/?view=netdesktop-7.0\" target=_blank>WPF.NET</A></P>\r\n<P style=\"FONT-SIZE: 20px\"><STRONG>For MORE info about Next Functionalities and Possibilities READ Documentation</STRONG></P>\r\n<P style=\"FONT-SIZE: 20px; MARGIN-TOP: 0px\"><STRONG>Example EDC Manager or ther Cilents you can <A href=\"/server/Downloads/\" target=_blank>Download</A> or <A href=\"https://kliknetezde.cz\" target=_blank>Watch Online</A> </STRONG></P></BODY></HTML>";

        }
    }
}