using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DiagramDesigner.Controls;

namespace DiagramDesigner
{
    //These attributes identify the types of the named parts that are used for templating
    [TemplatePart(Name = "PART_DragThumb", Type = typeof(DragThumb))]
    [TemplatePart(Name = "PART_ResizeDecorator", Type = typeof(Control))]
    //[TemplatePart(Name = "PART_ConnectorDecorator", Type = typeof(Control))]
    [TemplatePart(Name = "PART_ContentPresenter", Type = typeof(ContentPresenter))]
    public class DesignerItem : ContentControl, ISelectable, IGroupable
    {
        public static List<DesignerItem> List = new List<DesignerItem>();
        #region Commands
        public static RoutedCommand AyırKomutu = new RoutedCommand();
        public static RoutedCommand MergeCommand = new RoutedCommand();
        #endregion
        #region ID
        private Guid id;
        public Guid ID
        {
            get { return id; }
        }
        #endregion

        #region ParentID
        public Guid ParentID
        {
            get { return (Guid)GetValue(ParentIDProperty); }
            set { SetValue(ParentIDProperty, value); }
        }
        public static readonly DependencyProperty ParentIDProperty = DependencyProperty.Register("ParentID", typeof(Guid), typeof(DesignerItem));
        #endregion

        #region IsGroup
        public bool IsGroup
        {
            get { return (bool)GetValue(IsGroupProperty); }
            set { SetValue(IsGroupProperty, value); }
        }
        public static readonly DependencyProperty IsGroupProperty =
            DependencyProperty.Register("IsGroup", typeof(bool), typeof(DesignerItem));
        #endregion
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ForceAutoSize)
            {
                if (e.Property.Equals(WidthProperty))
                {
                    Width = double.NaN;
                }
                if (e.Property.Equals(HeightProperty))
                {
                    Height = double.NaN;
                } 
            }
            if(e.Property.Equals(IsSelectedProperty))
            {
                if (IsSelected)
                {
                    SelectedChanged?.Invoke(this, new EventArgs()); 
                }
            }
        }
        #region IsSelected Property
        public event EventHandler SelectedChanged;
        public bool ForceAutoSize
        {
            get { return (bool)GetValue(ForceAutoSizeProperty); }
            set { SetValue(ForceAutoSizeProperty, value); }
        }
        public static readonly DependencyProperty ForceAutoSizeProperty =
          DependencyProperty.Register("ForceAutoSize",
                                       typeof(bool),
                                       typeof(DesignerItem),
                                       new FrameworkPropertyMetadata(false));

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
        public static readonly DependencyProperty IsSelectedProperty =
          DependencyProperty.Register("IsSelected",
                                       typeof(bool),
                                       typeof(DesignerItem),
                                       new FrameworkPropertyMetadata(false));

        public bool DisableSelection
        {
            get { return (bool)GetValue(DisableSelectionProperty); }
            set { SetValue(DisableSelectionProperty, value); }
        }
        public static readonly DependencyProperty DisableSelectionProperty =
          DependencyProperty.Register("DisableSelection",
                                       typeof(bool),
                                       typeof(DesignerItem),
                                       new FrameworkPropertyMetadata(false));
        
        public bool Separate
        {
            get { return (bool)GetValue(SeparateProperty); }
            set { SetValue(SeparateProperty, value);
                SelectionService.Instance.ClearSelection();
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(SeparateProperty, Separate, Separate)); }
        }
        public static readonly DependencyProperty SeparateProperty =
          DependencyProperty.Register("Separate",
                                       typeof(bool),
                                       typeof(DesignerItem),
                                       new FrameworkPropertyMetadata(true));

        public DesignerItem SeparateOwner
        {
            get { return (DesignerItem)GetValue(SeparateOwnerProperty); }
            set { SetValue(SeparateOwnerProperty, value); }
        }
        public static readonly DependencyProperty SeparateOwnerProperty =
          DependencyProperty.Register("SeparateOwner",
                                       typeof(DesignerItem),
                                       typeof(DesignerItem));

        
        public DesignerCanvas CanvasOfSeparateOwner
        {
            get { return (DesignerCanvas)GetValue(CanvasOfSeparateOwnerProperty); }
            set { SetValue(CanvasOfSeparateOwnerProperty, value); }
        }
        public static readonly DependencyProperty CanvasOfSeparateOwnerProperty =
          DependencyProperty.Register("CanvasOfSeparateOwner",
                                       typeof(DesignerCanvas),
                                       typeof(DesignerItem));

        #endregion

        #region DragThumbTemplate Property

        // can be used to replace the default template for the DragThumb
        public static readonly DependencyProperty DragThumbTemplateProperty =
            DependencyProperty.RegisterAttached("DragThumbTemplate", typeof(ControlTemplate), typeof(DesignerItem));

        public static ControlTemplate GetDragThumbTemplate(UIElement element)
        {
            return (ControlTemplate)element.GetValue(DragThumbTemplateProperty);
        }

        public static void SetDragThumbTemplate(UIElement element, ControlTemplate value)
        {
            element.SetValue(DragThumbTemplateProperty, value);
        }

        #endregion

        //#region ConnectorDecoratorTemplate Property

        //// can be used to replace the default template for the ConnectorDecorator
        //public static readonly DependencyProperty ConnectorDecoratorTemplateProperty =
        //    DependencyProperty.RegisterAttached("ConnectorDecoratorTemplate", typeof(ControlTemplate), typeof(DesignerItem));

        //public static ControlTemplate GetConnectorDecoratorTemplate(UIElement element)
        //{
        //    return (ControlTemplate)element.GetValue(ConnectorDecoratorTemplateProperty);
        //}

        //public static void SetConnectorDecoratorTemplate(UIElement element, ControlTemplate value)
        //{
        //    element.SetValue(ConnectorDecoratorTemplateProperty, value);
        //}

        //#endregion

        #region IsDragConnectionOver

        // while drag connection procedure is ongoing and the mouse moves over 
        // this item this value is true; if true the ConnectorDecorator is triggered
        // to be visible, see template
        public bool IsDragConnectionOver
        {
            get { return (bool)GetValue(IsDragConnectionOverProperty); }
            set { SetValue(IsDragConnectionOverProperty, value); }
        }
        public static readonly DependencyProperty IsDragConnectionOverProperty =
            DependencyProperty.Register("IsDragConnectionOver",
                                         typeof(bool),
                                         typeof(DesignerItem),
                                         new FrameworkPropertyMetadata(false));

        #endregion

        static DesignerItem()
        {
            // set the key to reference the style for this control
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
                typeof(DesignerItem), new FrameworkPropertyMetadata(typeof(DesignerItem)));
        }

        public DesignerItem(Guid id)
        {
            this.id = id;
            this.CommandBindings.Add(new CommandBinding(AyırKomutu, Separate_Method));
            this.CommandBindings.Add(new CommandBinding(MergeCommand, Merge_Method));
            this.Loaded += new RoutedEventHandler(DesignerItem_Loaded);
            List.Add(this);
        }

        private void Separate_Method(object sender, ExecutedRoutedEventArgs e)
        {
            SeparateMethod();
        }
        public void SeparateMethod()
        {
            if (SeparateOwner != null)
                SeparateOwner.Separate = true;
            else
                Separate = true;
        }

        private void Merge_Method(object sender, ExecutedRoutedEventArgs e)
        {
            MergeMethod();
        }

        public void MergeMethod()
        {
            if (SeparateOwner != null)
                SeparateOwner.Separate = false;
            else
                Separate = false;
        }

        public DesignerItem()
            : this(Guid.NewGuid())
        {

        }


        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            if (DisableSelection)
                return;
            DesignerCanvas designer = VisualTreeHelper.GetParent(this) as DesignerCanvas;

            // update selection
            if (designer != null)
            {
                if ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) != ModifierKeys.None)
                    if (this.IsSelected)
                    {
                        designer.SelectionService.RemoveFromSelection(this);
                    }
                    else
                    {
                        designer.SelectionService.AddToSelection(this);
                    }
                else if (!this.IsSelected)
                {
                    designer.SelectionService.SelectItem(this);
                }
                Focus();
            }

            e.Handled = false;
        }

        void DesignerItem_Loaded(object sender, RoutedEventArgs e)
        {
            if (base.Template != null)
            {
                ContentPresenter contentPresenter =
                    this.Template.FindName("PART_ContentPresenter", this) as ContentPresenter;
                if (contentPresenter != null)
                {
                    UIElement contentVisual = VisualTreeHelper.GetChild(contentPresenter, 0) as UIElement;
                    if (contentVisual != null)
                    {
                        DragThumb thumb = this.Template.FindName("PART_DragThumb", this) as DragThumb;
                        if (thumb != null)
                        {
                            ControlTemplate template =
                                DesignerItem.GetDragThumbTemplate(contentVisual) as ControlTemplate;
                            if (template != null)
                                thumb.Template = template;
                        }
                    }
                }
            }
        }

        internal void SetZ(int value)
        {
            Canvas.SetZIndex(this, value);
            if (SeparateOwner != null)
            { 
                Canvas.SetZIndex(SeparateOwner, value);
                SeparateOwner.SetZ(value);
            }
            if (CanvasOfSeparateOwner != null)
            {
                Canvas.SetZIndex(CanvasOfSeparateOwner, value);
                if(CanvasOfSeparateOwner.Parent != null && CanvasOfSeparateOwner.Parent is UIElement)
                {
                    Canvas.SetZIndex(CanvasOfSeparateOwner.Parent as UIElement, value);
                }
            }

        }
    }
}
