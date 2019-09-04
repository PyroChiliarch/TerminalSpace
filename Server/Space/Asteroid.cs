using System;

namespace Server.Space
{


   

    class Asteroid : SpaceObject, IHealth
    {

        //TODO: WORK here
        //public int Healt



        public int Health
        {
            get;
            private set;

        }

        public int MaxHealth
        {
            get;
            private set;
        }



        //=============================================================================
        //Constructors
        //=============================================================================

        public Asteroid()
        {
            Name = "Asteroid";
            MaxHealth = 100;
            Health = MaxHealth;
        }

        public Asteroid(string name, int maxHealth)
        {
            Name = name;
            MaxHealth = maxHealth;
            Health = MaxHealth;

        }














        //=============================================================================
        //
        //=============================================================================


        public void AffectHealth(int delta)
        {
            Health += delta;

            //Kill self if no more health
            if (Health <= 0)
            {
                this.Kill();
            }
        }


        public void Kill()
        {
            OnDeath();
        }









        //=============================================================================
        //Event Methods
        //=============================================================================

        public void OnDeath()
        {

            //Checks if equal to null before calling DeathEvent
            DeathEvent?.Invoke(this, EventArgs.Empty);

            Destroy();
        }

        public event EventHandler DeathEvent;
    }
}
