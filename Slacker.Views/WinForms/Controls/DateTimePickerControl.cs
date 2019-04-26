using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace Slacker.Views.Winforms.Controls {
    public partial class DateTimePickerControl : UserControl {
        

        private Color _secondaryColor = Color.FromArgb(54, 189, 255);
        public Color SecondaryColor {
            get => _secondaryColor;
            set {
                _secondaryColor = value;
                panelSeperator.BackColor = value;
            }
        }

        protected CultureInfo Culture => new CultureInfo("en-US");

        private DateTime _date;
        public DateTime Date {
            get => _date;
            set {
                var updateRequired = _date == null ||
                    _date.Month != value.Month ||
                    _date.Year != value.Year;

                _date = value;

                DateChanged?.Invoke(this, null);
                UpdateCalendar(updateRequired);
            }
        }

        /// <summary>
        /// Seeks back to the first Sunday from the first day calendar day of month
        /// </summary>
        protected DateTime FirstCalendarDay {
            get {
                var firstDay = new DateTime(Date.Year, Date.Month, 1);
                while (firstDay.DayOfWeek != DayOfWeek.Sunday) {
                    firstDay = firstDay.AddDays(-1);
                }

                return firstDay;
            }
        }

        public event EventHandler DateChanged;

        public DateTimePickerControl() {
            InitializeComponent();
            RegisterTimeControls();
            Date = DateTime.Now;
        }

        protected class TimeControl {
            public Control Control;
            public Func<DateTime, bool> IsSelected;
            public Func<DateTime, DateTime> TimeTravel;
        }


        protected void RegisterTimeControls() {
            TimeControls = new List<TimeControl>();

            // Register Hour Controls
            int hours = -1;
            for (var row = 0; row < tableHourControls.RowCount; row++) {
                for (var col = 0; col < tableHourControls.ColumnCount; col++) {
                    var control = tableHourControls.GetControlFromPosition(col, row);
                    TimeControls.Add(GetHourControl(control, hours += 1));
                }
            }

            // Register Minute Controls
            int minutes = -5;
            for (var row = 0; row < tableMinuteControls.RowCount; row++) {
                for (var col = 0; col < tableMinuteControls.ColumnCount; col++) {
                    var control = tableMinuteControls.GetControlFromPosition(col, row);
                    TimeControls.Add(GetMinuteControl((Control)control, minutes += 5));
                }
            }
        }

        private TimeControl GetHourControl(Control control, int controlHrs) {
            return new TimeControl() {
                Control = control,
                IsSelected = (date) => {
                    // Convert to 12 hour time
                    int hrs = Date.Hour;
                    if (hrs >= 12) {
                        hrs = hrs - 12;
                    }

                    return hrs == controlHrs;
                },
                TimeTravel = (date) => {
                    // Convert to 24 hour time
                    var hrs = date.Hour > 12 ?
                        12 + controlHrs : controlHrs;

                    return new DateTime(
                        date.Year, date.Month, date.Day,
                        hrs, date.Minute, date.Second
                    );
                }
            };
        }

        private TimeControl GetMinuteControl(Control control, int controlMins) {
            return new TimeControl() {
                Control = control,
                IsSelected = date => (Math.Floor(date.Minute / 5.0) * 5.0) == controlMins,
                TimeTravel = date => new DateTime(
                    date.Year, date.Month, date.Day,
                    date.Hour, controlMins, date.Second
                )
            };
        }

        protected List<TimeControl> TimeControls;

        protected class DateControl {
            public Label Label;
            public DateTime DateTime;
        }

        protected List<DateControl> DateControls;

        protected void UpdateCalendar(bool updateStructure) {
            if (updateStructure) {
                UpdateCalendarStructure();
            }

            // Update AM/PM
            labelAPM.Text = Date.ToString("tt", Culture);

            // Update Calendar Month/Year
            labelMonYear.Text = Date.ToString("MMMM, yyyy", Culture);

            // Update Date Controls
            DateControls.ForEach(dateControl => {
                var label = dateControl.Label;
                var date = dateControl.DateTime;

                label.Text = date.Day.ToString();
                
                // Format date styling based on selected date
                if (date.Month == Date.Month) {
                    // Selected Month Date
                    if (date.Day == Date.Day) {
                        label.BackColor = SecondaryColor;
                        label.ForeColor = Color.White;
                        return;
                    }
                    // Non-Selected Month Date
                    else { 
                        label.ForeColor = ForeColor;
                        label.BackColor = BackColor;
                    }
                }
                // Non-Month Dates
                else { 
                    label.ForeColor = SecondaryColor;
                    label.BackColor = BackColor;
                }
            });

            TimeControls.ForEach(timeControl => {
                if (timeControl.IsSelected(Date)) {
                    timeControl.Control.ForeColor = Color.White;
                    timeControl.Control.BackColor = SecondaryColor;
                }
                else {
                    timeControl.Control.ForeColor = ForeColor;
                    timeControl.Control.BackColor = BackColor;
                }

            });
        }

        private void UpdateCalendarStructure() {
            DateControls = new List<DateControl>();

            // Returns the next calendar date in sequence
            var nextCalendarDate = FirstCalendarDay;
            Func<DateTime> getNextCalendarDate = () => {
                var date = nextCalendarDate;
                nextCalendarDate = nextCalendarDate.AddDays(1);
                return date;
            };

            for (int row = 1; row < calendarTable.RowCount; row++) {
                for (int col = 0; col < calendarTable.ColumnCount; col++) {
                    var dateControl = new DateControl() { 
                        Label = (Label) calendarTable.GetControlFromPosition(col, row),
                        DateTime = getNextCalendarDate()
                    };

                    DateControls.Add(dateControl);
                }
            }
        }

        private void IconSeekPrevYear_Click(object sender, EventArgs e) {
            Date = Date.AddYears(-1);
        }

        private void IconSeekPrevMonth_Click(object sender, EventArgs e) {
            Date = Date.AddMonths(-1);
        }

        private void IconSeekMonth_Click(object sender, EventArgs e) {
            Date = Date.AddMonths(1);
        }

        private void IconSeekYear_Click(object sender, EventArgs e) {
            Date = Date.AddYears(1);
        }

        private void IconToggleAPM_Click(object sender, EventArgs e) {
            Date = Date.AddHours(Date.Hour >= 12 ? -12 : 12);
        }

        private void DateLabel_Click(object sender, EventArgs e) {
            var dateControl = DateControls.FirstOrDefault(dc => dc.Label == sender);
            if (dateControl == null) {
                return;
            }

            this.Date = new DateTime(
                dateControl.DateTime.Year,
                dateControl.DateTime.Month,
                dateControl.DateTime.Day,
                Date.Hour,
                Date.Minute,
                Date.Second
            );
        }

        private void TimeLabel_Click(object sender, EventArgs e) {
            var timeControl = TimeControls.FirstOrDefault(tc => tc.Control == sender);
            if (timeControl == null) {
                return;
            }

            this.Date = timeControl.TimeTravel(Date);
        }
        
    }
}
