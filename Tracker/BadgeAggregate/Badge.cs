using System;

namespace HabitTracker.Tracker{
    public class Badge{
        private Guid _id;
        private string _name;
        private string _description;
        private Guid _user_id;
        private DateTime _created_at;

        public Guid ID{
            get{
                return _id;
            }
        }

        public string Name{
            get{
                return _name;
            }
        }

        public string Description{
            get{
                return _description;
            }
        }

        public Guid UserId{
            get{
                return _user_id;
            }
        }

        public DateTime CreatedAt{
            get{
                return _created_at;
            }
        }

        public Badge(Guid id, string name, string description, Guid user_id, DateTime created_at){
            this._id = id;
            this._name = name;
            this._description = description;
            this._user_id = user_id;
            this._created_at = created_at;
        }


    }
    
}