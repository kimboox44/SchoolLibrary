using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarianClient
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Windows.Controls;

    using LibrarianClient.Service;

    public class ReaderViewModel : INotifyPropertyChanged
    {
        #region regex
        private readonly Regex nameEx = new Regex("[A-Za-zА-Яа-я]{2,}");

        private readonly Regex phoneEx = new Regex("^[+38-]?[0-9]{3}-[0-9]{3}-[0-9]{2}-[0-9]{2}");  
        
        private readonly Regex emailEx = new Regex("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$");
        #endregion

        #region fields
        private string firstName;

        private string lastName;

        private string address;

        private DateTime birthday;

        private string phone;

        private string email;

        #endregion

        #region properties

        public int ReaderId { get; set; }

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (value == null || !nameEx.IsMatch(value))
                {
                    throw new ArgumentException("Incorrect first name");
                }

                firstName = value;
                this.NotifyPropertyChanged();
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                if (value == null || !nameEx.IsMatch(value))
                {
                    throw new ArgumentException("Incorrect last name");
                }

                lastName = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                if (value == null )
                {
                    throw new ArgumentException("Incorrect address");
                }

                address = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                if (value == null || !phoneEx.IsMatch(value))
                {
                    throw new ArgumentException("Incorrect phone number");
                }

                phone = value;
                this.NotifyPropertyChanged();
            }
        }

        public string EMail
        {
            get
            {
                return email;
            }
            set
            {
                if (value == null || !emailEx.IsMatch(value))
                {
                    throw new ArgumentException("Incorrect email");
                }

                email = value;
                this.NotifyPropertyChanged();
            }
        }

        public DateTime Birthday
        {
            get
            {
                return birthday;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Incorrect birthday");
                }

                birthday = value;
                this.NotifyPropertyChanged();
            }
        }
        #endregion

        #region operators
        public static explicit operator ReaderViewModel(ReaderBusinessModel reader)
        {
            var readerViewModel = new ReaderViewModel
                                      {
                                          ReaderId = reader.ReaderId,
                                          FirstName = reader.FirstName,
                                          LastName = reader.LastName,
                                          Address = reader.Address,
                                          Birthday = reader.Birthday.Value,
                                          EMail = reader.EMail,
                                          Phone = reader.Phone
                                      };
            return readerViewModel;
        }

        public static explicit operator ReaderBusinessModel(ReaderViewModel reader)
        {
            var readerBusinessModel = new ReaderBusinessModel
            {
                ReaderId = reader.ReaderId,
                FirstName = reader.FirstName,
                LastName = reader.LastName,
                Address = reader.Address,
                Birthday = reader.Birthday,
                EMail = reader.EMail,
                Phone = reader.Phone
            };
            return readerBusinessModel;
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
