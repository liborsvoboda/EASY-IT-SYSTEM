using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;
using MahApps.Metro.Controls;

namespace EasyITSystemCenter.GlobalOperations {

    /// <summary>
    /// Centralised Forms Functions for work with Types, methods, Formats, Logic, supported methods
    /// </summary>
    internal class FormOperations {


        /// <summary>
        /// Globa Translator for Objects with Defined Name as: xx_translatedString
        /// Prepared for MenuItem, Label, Button, GroupBox, TabItems
        /// </summary>
        /// <param name="parentObject"></param>
        /// <returns></returns>
        public static async Task<bool> TranslateFormFields(DependencyObject parentObject) {
            try {
                foreach (var element in FindVisualChildren<Grid>(parentObject)) {
                    element.Children.OfType<Label>().ToList().ForEach(async item => {
                        try { if (item.Name.Split('_').Length > 1 && item.Content == null) { item.Content = await DBOperations.DBTranslation(item.Name.Split('_')[1]); } } catch { }
                    });
                    element.Children.OfType<Button>().ToList().ForEach(async item => {
                        try { if (item.Name.Split('_').Length > 1 && item.Content == null) { item.Content = await DBOperations.DBTranslation(item.Name.Split('_')[1]); } } catch { }
                    });
                    //TODO REMOVE CheckBox LAbels AND CHECK DIRECT LABELS FOR NUMERIC? TEXTBOX SAME AS CHECKBOX
                    // THIS ENABLE SHOW CheckBox Own Label Directly
                    //element.FindChildren<CheckBox>().ToList().ForEach(async item => {
                    //    try { if (item.Name.Split('_').Length > 1 && item.Content == null) { item.Content = await DBOperations.DBTranslation(item.Name.Split('_')[1]); } } catch { }
                    //});
                }
                foreach (var element in FindVisualChildren<TabControl>(parentObject)) {
                    element.Items.OfType<TabItem>().ToList().ForEach(async item => {
                        try { if (item.Name.Split('_').Length > 1 && item.Header == null) { item.Header = await DBOperations.DBTranslation(item.Name.Split('_')[1]); } } catch { }
                    });
                }
                foreach (var element in FindVisualChildren<GroupBox>(parentObject)) { 
                    try { if (element.Name.Split('_').Length > 1 && element.Header == null) { element.Header = await DBOperations.DBTranslation(element.Name.Split('_')[1]); } } catch { }
                    element.FindChildren<Label>().ToList().ForEach(async item => {
                        try { if (item.Name.Split('_').Length > 1 && item.Content == null) { item.Content = await DBOperations.DBTranslation(item.Name.Split('_')[1]); } } catch { }
                    });
                    element.FindChildren<Button>().ToList().ForEach(async item => {
                        try { if (item.Name.Split('_').Length > 1 && item.Content == null) { item.Content = await DBOperations.DBTranslation(item.Name.Split('_')[1]); } } catch { }
                    });
                    element.FindChildren<CheckBox>().ToList().ForEach(async item => {
                        try { if (item.Name.Split('_').Length > 1 && item.Content == null) { item.Content = await DBOperations.DBTranslation(item.Name.Split('_')[1]); } } catch { }
                    });
                }
                foreach (var element in FindVisualChildren<Label>(parentObject)) {
                    try { if (element.Name.Split('_').Length > 1 && element.Content == null) { element.Content = await DBOperations.DBTranslation(element.Name.Split('_')[1]); } } catch { }
                }
                foreach (var element in FindVisualChildren<MenuItem>(parentObject)) {
                    try { if (element.Header == null) { element.Header = await DBOperations.DBTranslation(element.Name.Split('_')[1]); } } catch { }
                }
                foreach (var element in FindVisualChildren<Button>(parentObject)) {
                    try { if (element.Name.Split('_').Length > 1 && element.Content == null) { element.Content = await DBOperations.DBTranslation(element.Name.Split('_')[1]); } } catch { }
                }
                foreach (var element in FindVisualChildren<Button>(parentObject)) {
                    try { if (element.Name.Split('_').Length > 1 && element.Content == null) { element.Content = await DBOperations.DBTranslation(element.Name.Split('_')[1]); } } catch { }
                }
               try {
                    if (parentObject.GetType().Name == "ToolBar") {
                        (parentObject as ToolBar).Items.OfType<Label>().ToList().ForEach(async item => {
                            try { if (item.Name.Split('_').Length > 1 && item.Content == null) { item.Content = await DBOperations.DBTranslation(item.Name.Split('_')[1]); } } catch { }
                        });
                    }
                } catch { }
                try {
                    if (parentObject.GetType().Name == "ContextMenu") {
                        (parentObject as ContextMenu).Items.OfType<MenuItem>().ToList().ForEach(async item => {
                            try { if (item.Name.Split('_').Length > 1 && item.Header == null) { item.Header = await DBOperations.DBTranslation(item.Name.Split('_')[1]); } } catch { }
                        });
                    }
                } catch { }
            } catch (Exception Ex) { App.ApplicationLogging(Ex); }
            return true;
        }


        /// <summary>
        /// Translate Toolbar Items Name to Tooltip Description
        /// Buttons, CheckBox, ComboBox
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parentObject"></param>
        /// <returns></returns>
        public static async Task<bool> TranslateSubObjectsNameToToolTip<T>(T parentObject) {
            try {
                if (parentObject.GetType().Name.ToLower() == "toolbar") {
                    (parentObject as ToolBar).Items.OfType<CheckBox>().ToList().ForEach(async checkbox => {
                        try { if (checkbox.ToolTip == null) { checkbox.ToolTip = await DBOperations.DBTranslation(checkbox.Name.Split('_')[1]); } } catch { }
                    });
                    (parentObject as ToolBar).Items.OfType<Button>().ToList().ForEach(async button => {
                        try { if (button.ToolTip == null) { button.ToolTip = await DBOperations.DBTranslation(button.Name.Split('_')[1]); } } catch { }
                    });
                    (parentObject as ToolBar).Items.OfType<ComboBox>().ToList().ForEach(async combobox => {
                        try { if (combobox.ToolTip == null) { combobox.ToolTip = await DBOperations.DBTranslation(combobox.Name.Split('_')[1]); } } catch { }
                    });
                }
                if (parentObject.GetType().Name.ToLower() == "checkbox") {
                    try { if ((parentObject as CheckBox).ToolTip == null) { (parentObject as CheckBox).ToolTip = await DBOperations.DBTranslation((parentObject as CheckBox).Name.Split('_')[1]); } } catch { }
                }
                if (parentObject.GetType().Name.ToLower() == "button") {
                    try { if ((parentObject as Button).ToolTip == null) { (parentObject as Button).ToolTip = await DBOperations.DBTranslation((parentObject as Button).Name.Split('_')[1]); } } catch { }
                }
                if (parentObject.GetType().Name.ToLower() == "combobox") {
                    try { if ((parentObject as ComboBox).ToolTip == null) { (parentObject as ComboBox).ToolTip = await DBOperations.DBTranslation((parentObject as ComboBox).Name.Split('_')[1]); } } catch { }
                }
            } catch (Exception Ex) { App.ApplicationLogging(Ex); }
            return true;
        }


        /// <summary>
        /// TextInput allowing Analphabetical and numeric character Only
        /// </summary>
        public static bool AnalphabetAndNumericOnlyValidated(ref TextCompositionEventArgs e, bool onlyLowerChars) {
            Regex regex = onlyLowerChars ? new Regex("[^a-z0-9]+") : new Regex("[^a-zA-Z0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            return e.Handled;
        }

        /// <summary>
        /// Text Input Allowing Numeric Characters Only
        /// </summary>
        public static bool NumberOnlyValidated(string checkedText) {
            Regex regex = new Regex("[^0-9]+");
            return regex.IsMatch(checkedText);
        }

        /// <summary>
        /// Removes the disabled spaces from referenced Inpout.
        /// </summary>
        public static void RemoveDisabledSpacesFromTextInput(ref object sender, ref TextChangedEventArgs e) {
            if (((TextBox)e.Source).Text.Length != ((TextBox)e.Source).Text.Replace(" ", "").Length) {
                ((TextBox)sender).Text = ((TextBox)e.Source).Text.Replace(" ", ""); ((TextBox)sender).Select(((TextBox)sender).Text.Length, 0);
            }
        }


        /// <summary>
        /// Secventional Show All Tooltips On
        /// Shown Objects in System
        /// </summary>
        /// <param name="userControl"></param>
        public async static void DisplayAllToolTips_OnClick(FrameworkElement userControl) {
            foreach (var element in FindVisualChildren<FrameworkElement>(userControl)) {
                
                if (element.ToolTip == null) { continue; }
                if(!element.IsVisible) { continue; }

                var toolTip = element.ToolTip as ToolTip ??
                              new ToolTip { Content = element.ToolTip };
                try {
                    element.ToolTip = toolTip;
                    if (element.ToolTip != null && element.ToolTip.ToString().Length > 3) {
                        toolTip.PlacementTarget = element;
                        toolTip.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#e1a05d");
                        toolTip.Placement = PlacementMode.Relative;
                        toolTip.IsOpen = true;
                        await Task.Delay(TimeSpan.FromSeconds(1));
                    }
                } catch { }
                try {
                    if (element.ToolTip != null && element.ToolTip.ToString().Length > 0) {
                        toolTip.PlacementTarget = element;
                        toolTip.IsOpen = false;
                    }
                } catch { }
            }
        }


        /// <summary>
        /// Get All Children of Root-Parent Object by Type
        /// Using for Global Translating, Change Properties, etc.. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="depObj"></param>
        /// <returns></returns>
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject {
            if (depObj == null) yield break;

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++) {
                var child = VisualTreeHelper.GetChild(depObj, i);
                if (child is T t) yield return t;
                foreach (var childOfChild in FindVisualChildren<T>(child)) yield return childOfChild;
            }
        }
    }
}