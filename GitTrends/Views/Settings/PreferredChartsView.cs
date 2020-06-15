﻿using System.Linq;
using GitTrends.Mobile.Common;
using GitTrends.Mobile.Common.Constants;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;
using Xamarin.Forms.Markup;
using static GitTrends.XamarinFormsService;
using static Xamarin.Forms.Markup.GridRowsColumns;

namespace GitTrends
{
    class PreferredChartsView : Grid
    {
        public PreferredChartsView(SettingsViewModel settingsViewModel)
        {
            RowSpacing = 2;

            VerticalOptions = LayoutOptions.End;

            RowDefinitions = Rows.Define(
                (Row.Label, StarGridLength(1)),
                (Row.Control, StarGridLength(2)));

            Children.Add(new PreferredChartSettingsLabel().Row(Row.Label));
            Children.Add(new PreferredChartSettingsControl(settingsViewModel).Row(Row.Control));
        }

        enum Row { Label, Control }

        class PreferredChartSettingsLabel : TitleLabel
        {
            public PreferredChartSettingsLabel()
            {
                Text = SettingsPageConstants.PreferredChartSettingsLabelText;
                AutomationId = SettingsPageAutomationIds.TrendsChartSettingsLabel;
                VerticalTextAlignment = TextAlignment.Start;
            }
        }

        class PreferredChartSettingsControl : SfSegmentedControl
        {
            const double cornerRadius = 4;
            readonly SettingsViewModel _settingsViewModel;

            public PreferredChartSettingsControl(in SettingsViewModel settingsViewModel)
            {
                _settingsViewModel = settingsViewModel;

                CornerRadius = cornerRadius;
                AutomationId = SettingsPageAutomationIds.TrendsChartSettingsControl;
                ItemsSource = TrendsChartConstants.TrendsChartTitles.Values.ToList();
                VisibleSegmentsCount = TrendsChartConstants.TrendsChartTitles.Values.Count;
                SelectedIndex = _settingsViewModel.PreferredChartsSelectedIndex;
                SelectionIndicatorSettings = new TrendsChartSettingsSelectionIndicatorSettings();
                FontFamily = FontFamilyConstants.RobotoMedium;
                FontSize = 12;

                SetDynamicResource(FontColorProperty, nameof(BaseTheme.BorderButtonFontColor));
                SetDynamicResource(BorderColorProperty, nameof(BaseTheme.BorderButtonBorderColor));
                SetDynamicResource(BackgroundColorProperty, nameof(BaseTheme.PageBackgroundColor));

                this.SetBinding(SelectedIndexProperty, nameof(SettingsViewModel.PreferredChartsSelectedIndex));
            }

            class TrendsChartSettingsSelectionIndicatorSettings : SelectionIndicatorSettings
            {
                public TrendsChartSettingsSelectionIndicatorSettings()
                {
                    CornerRadius = cornerRadius;
                    SetDynamicResource(ColorProperty, nameof(BaseTheme.TrendsChartSettingsSelectionIndicatorColor));
                }
            }
        }
    }
}
