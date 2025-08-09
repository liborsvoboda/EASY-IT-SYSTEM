using MahApps.Metro.Controls;
using Microsoft.Xaml.Behaviors;
using NanoByte.Common.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;


namespace EasyITSystemCenter.GlobalStyles {


    /// <summary>
    /// TextBox Behavior SelectAll on Focus
    /// Example of Link in XAML
    /// xmlns:XamlExt="clr-namespace:EasyITSystemCenter.ProgramaticXamlBehaviors"
    /// in TextBox Add behaviors:TextBoxBehavior.SelectAllTextOnFocus="True"
    /// </summary>
    public class TextBoxBehavior : Behavior<TextBox> {

        /// <summary>
        /// Xaml Extension Get Select All On Focus
        /// </summary>
        /// <param name="textBox"></param>
        /// <returns></returns>
        public static bool GetSelectAllTextOnFocus(TextBox textBox) {
            return (bool)textBox.GetValue(SelectAllTextOnFocusProperty);
        }

        /// <summary>
        /// Xaml Extension Set Select All On Focus
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="value"></param>
        public static void SetSelectAllTextOnFocus(TextBox textBox, bool value) {
            textBox.SetValue(SelectAllTextOnFocusProperty, value);
        }

        /// <summary>
        /// Xaml Property: SelectAllTextOnFocus  
        /// </summary>
        public static readonly DependencyProperty SelectAllTextOnFocusProperty =
            DependencyProperty.RegisterAttached(
                "SelectAllTextOnFocus",
                typeof(bool),
                typeof(TextBoxBehavior),
                new UIPropertyMetadata(false, OnSelectAllTextOnFocusChanged));

        /// <summary>
        /// Ons the select all text on focus changed.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The e.</param>
        private static void OnSelectAllTextOnFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var textBox = d as TextBox;
            if (textBox == null) return;

            if (e.NewValue is bool == false) return;

            if ((bool)e.NewValue) {
                textBox.GotFocus += SelectAll;
                textBox.PreviewMouseDown += IgnoreMouseButton;
            }
            else {
                textBox.GotFocus -= SelectAll;
                textBox.PreviewMouseDown -= IgnoreMouseButton;
            }
        }

        /// <summary>
        /// Selects the all.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private static void SelectAll(object sender, RoutedEventArgs e) {
            var textBox = e.OriginalSource as TextBox;
            if (textBox == null) return;
            textBox.SelectAll();
        }

        /// <summary>
        /// Ignores the mouse button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private static void IgnoreMouseButton(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            var textBox = sender as TextBox;
            if (textBox == null || textBox.IsKeyboardFocusWithin) return;

            e.Handled = true;
            textBox.Focus();
        }
    }


    /// <summary>
    /// Style Includer Definition, 
    /// merge more styles to One For Use  Metro Default Definiton + Custom Modifications
    /// Using Example:
    /// <Button Style="{XamlExt:MergedStyles BasedOn={StaticResource FirstStyle} MergeStyle={ StaticResource SecondStyle}}" />
    /// </summary>
    [MarkupExtensionReturnType(typeof(Style))]
    public class MergedStylesExtension : MarkupExtension {
        public Style BasedOn { get; set; }
        public Style MergeStyle { get; set; }

        public override object ProvideValue(IServiceProvider
                                            serviceProvider) {
            if (null == MergeStyle)
                return BasedOn;

            Style newStyle = new Style(BasedOn.TargetType,
                                       BasedOn);

            MergeWithStyle(newStyle, MergeStyle);

            return newStyle;
        }

        private static void MergeWithStyle(Style style,
                                           Style mergeStyle) {
            // Recursively merge with any Styles this Style might be BasedOn.
            if (mergeStyle.BasedOn != null) {
                MergeWithStyle(style, mergeStyle.BasedOn);
            }

            // Merge the Setters...
            foreach (var setter in mergeStyle.Setters)
                style.Setters.Add(setter);

            // Merge the Triggers...
            foreach (var trigger in mergeStyle.Triggers)
                style.Triggers.Add(trigger);
        }
    }


    public class StyleHolder {
        public StyleHolder() { }
        public StyleHolder(Style style) { Style = style; }
        public Style Style { get; set; }
    }



    public class CompoundStyle : DependencyObject {
        public static readonly DependencyProperty AttachedProperty =
            DependencyProperty.RegisterAttached("Attached", typeof(CompoundStyle), typeof(CompoundStyle), new PropertyMetadata(null, DependencyPropertyChanged));
        public static readonly DependencyProperty StyleKeysProperty =
            DependencyProperty.RegisterAttached("StyleKeys", typeof(string), typeof(CompoundStyle), new PropertyMetadata(null, DependencyPropertyChanged));

        public static CompoundStyle GetAttached(FrameworkElement obj) {
            return (CompoundStyle)obj.GetValue(AttachedProperty);
        }

        public static void SetAttached(FrameworkElement obj, CompoundStyle value) {
            obj.SetValue(AttachedProperty, value);
        }

        public static string GetStyleKeys(FrameworkElement obj) {
            return (string)obj.GetValue(StyleKeysProperty);
        }

        public static void SetStyleKeys(FrameworkElement obj, string value) {
            obj.SetValue(StyleKeysProperty, value);
        }

        private Style m_style;
        private Style[] m_inlineStyles = new Style[5];
        public List<StyleHolder> BasedOnStyles { get; set; }


        /// <summary>
        /// Compouding More Defined Static Or Dynamic Styles To One For Easy Using Definitions
        /// Using: <TextBlock XamlExt:CompoundStyle.StyleKeys="headerStyle,textForMessageStyle,centeredStyle"/>
        /// Its Retaken Not Tested
        /// </summary>
        public CompoundStyle() { BasedOnStyles = new List<StyleHolder>(); }

        public Style Style1 {
            get { return m_inlineStyles[0]; }
            set { m_inlineStyles[0] = value; }
        }

        public Style Style2 {
            get { return m_inlineStyles[1]; }
            set { m_inlineStyles[1] = value; }
        }

        public Style Style3 {
            get { return m_inlineStyles[2]; }
            set { m_inlineStyles[2] = value; }
        }

        public Style Style4 {
            get { return m_inlineStyles[3]; }
            set { m_inlineStyles[3] = value; }
        }

        public Style Style5 {
            get { return m_inlineStyles[4]; }
            set { m_inlineStyles[4] = value; }
        }


        public Style Style {
            get { if (m_style == null) { GenerateStyle(); } return m_style; }
        }

        private static void DependencyPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args) {
            if (args.Property == AttachedProperty) {
                ((FrameworkElement)obj).Style = ((CompoundStyle)args.NewValue).Style;
            }
            else if (args.Property == StyleKeysProperty) {
                FrameworkElement fw = (FrameworkElement)obj;
                RoutedEventHandler loadedEvent = null;
                loadedEvent = (s, e) => StyleKeysElement_Loaded(fw, (string)args.NewValue, (string)args.OldValue, loadedEvent);
                fw.Loaded += loadedEvent;
            }
        }

        private static void StyleKeysElement_Loaded(FrameworkElement fw, string newValue, string oldValue, RoutedEventHandler ev) { fw.Loaded -= ev;
            if (newValue == GetStyleKeys(fw)) { HandleStyleStrings(fw, newValue); }
        }


        private static void HandleStyleStrings(FrameworkElement fw, string newValue) {
            CompoundStyle result = new CompoundStyle(); string stylesString = newValue; string[] keys = stylesString.Split(',');

            foreach (var key in keys.Select(x => x.Trim())) {
                object resourceObject = fw.TryFindResource(key.Trim());
                if (resourceObject == null) {
                    App.ApplicationLogging(new Exception($"CompoundStyle: Could not find Resource: '{key}' when building style for FrameworkElement of type {fw.GetType().Name}, named {fw.Name}"));
                    break;
                }

                Style style = resourceObject as Style;
                if (style == null) {
                    App.ApplicationLogging(new Exception($"CompoundStyle: Resource: '{key}' is not a Style (when building style for FrameworkElement of type: {fw.GetType().Name}, named: {fw.Name})"));
                    break;
                }
                result.BasedOnStyles.Add(new StyleHolder(style));
            }
            fw.Style = result.Style;
        }


        private void GenerateStyle() {
            Dictionary<DependencyProperty, object> setters = new Dictionary<DependencyProperty, object>(); Style first = null;

            foreach (var style in m_inlineStyles.Union(BasedOnStyles.Select(x => x.Style)).Where(x => x != null)) {
                if (first == null) { first = style; }

                foreach (var setter in style.Setters.OfType<Setter>()) {
                    setters[setter.Property] = setter.Value;
                }
            }

            if (first != null) { Type targetType = first.TargetType; Style result = new Style(targetType);
                foreach (var pairs in setters) {
                    result.Setters.Add(new Setter(pairs.Key, pairs.Value));
                }
                m_style = result;
            }
        }
    }


	//ListBoxProperty Set/Get AutoScroll on Defined Listbox Example ImageListbox show selected image by other input
	//Property Name: SetAutoScrollToCurrentItem="True" GetAutoScrollToCurrentItem
	public class ListBoxAutoScrollBehavior
    {
        public static readonly DependencyProperty AutoScrollToCurrentItemProperty
          = DependencyProperty.RegisterAttached("AutoScrollToCurrentItem",
                                                typeof(bool),
                                                typeof(ListBoxAutoScrollBehavior),
                                                new UIPropertyMetadata(default(bool), OnAutoScrollToCurrentItemChanged));

        /// <summary>
        /// Returns the value of the AutoScrollToCurrentItemProperty
        /// </summary>
        /// <param name="obj">The dependency-object whichs value should be returned</param>
        /// <returns>The value of the given property</returns>
        public static bool GetAutoScrollToCurrentItem(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoScrollToCurrentItemProperty);
        }

        /// <summary>
        /// Sets the value of the AutoScrollToCurrentItemProperty
        /// </summary>
        /// <param name="obj">The dependency-object whichs value should be set</param>
        /// <param name="value">The value which should be assigned to the AutoScrollToCurrentItemProperty</param>
        public static void SetAutoScrollToCurrentItem(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoScrollToCurrentItemProperty, value);
        }

        /// <summary>
        /// This method will be called when the AutoScrollToCurrentItem
        /// property was changed
        /// </summary>
        /// <param name="s">The sender (the ListBox)</param>
        /// <param name="e">Some additional information</param>
        public static void OnAutoScrollToCurrentItemChanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var listBox = s as ListBox;
            if (listBox != null)
            {
                var listBoxItems = listBox.Items;
                if (listBoxItems != null)
                {
                    var newValue = (bool)e.NewValue;

                    var autoScrollToCurrentItemWorker = new EventHandler((s1, e2) => OnAutoScrollToCurrentItem(listBox, listBox.Items.CurrentPosition));

                    if (newValue)
                    {
                        listBoxItems.CurrentChanged += autoScrollToCurrentItemWorker;
                    }
                    else
                    {
                        listBoxItems.CurrentChanged -= autoScrollToCurrentItemWorker;
                    }
                }
            }
        }

        /// <summary>
        /// This method will be called when the ListBox should
        /// be scrolled to the given index
        /// </summary>
        /// <param name="listBox">The ListBox which should be scrolled</param>
        /// <param name="index">The index of the item to which it should be scrolled</param>
        public static void OnAutoScrollToCurrentItem(ListBox listBox, int index)
        {
            if (listBox != null && listBox.Items != null && listBox.Items.Count > index && index >= 0)
            {
                listBox.ScrollIntoView(listBox.Items[index]);
            }
        }
    }

}