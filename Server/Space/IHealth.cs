using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Space
{
    public interface IHealth
    {
        int Health
        {
            get;
        }

        int MaxHealth
        {
            get;
        }

        void AffectHealth(int delta);

        void Kill();


        void OnDeath();

        event EventHandler DeathEvent;

        
    }
}
