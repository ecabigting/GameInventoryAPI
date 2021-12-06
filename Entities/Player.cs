using System;

namespace GameInventory.Entities 
{
    public class Player 
    {
        public Guid Id {get; set;}
        public string Name {get;set;}
        public int Level {get;set;}
        public PrimaryStats PrimeStats {get;set;}     
        public SecondaryStats SecStats {get;set;}  
        
        public Player(){}
        public SecondaryStats GetSecStats()
        {
            return new(){
                BaseArmor = ((this.PrimeStats.Strength)* 0.02m) * this.Level,
                DodgeRate = ((this.PrimeStats.Agility)* 0.02m) * this.Level,
                Health = ((this.PrimeStats.Strength)* 0.02m) * this.Level,
                HitRate = ((this.PrimeStats.Dexterity)* 0.02m) * this.Level,
                Mana = ((this.PrimeStats.Intelligence)* 0.02m) * this.Level
            };
        }

    }
    
    public record PrimaryStats 
    {
        public int Strength {get;init;}
        public int Agility { get; init; }
        public int Dexterity {get;init;}
        public int Intelligence {get;init;}        
    }

    public record SecondaryStats 
    {
        public decimal Health {get;init;}
        public decimal Mana {get;init;}
        public decimal HitRate {get;init;}
        public decimal DodgeRate {get;init;}
        public decimal BaseArmor {get;init;}
        
    }
}