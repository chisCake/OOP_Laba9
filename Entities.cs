using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Laba9 {
	abstract class Entity {
		public readonly string Name;
		public readonly double BaseHP;
		public double HP { get; private set; }
		public double AttackValue { get; private set; }
		public double DodgeChance { get; private set; }
		public double HitChance { get; private set; }
		public double Defence { get; private set; }

		static Random rndm = new Random();

		public Entity(string name, double hp, double attackValue, double dodgeChance, double hitChance, double defence) {
			Name = name;
			HP = BaseHP = hp;
			AttackValue = attackValue;
			DodgeChance = dodgeChance;
			HitChance = hitChance;
			Defence = defence;
		}

		public static (double, bool) Attacked(Entity target, Entity attacker) {
			if (rndm.Next(0, 100) > attacker.HitChance * (1 - target.DodgeChance) * 100)
				return (0, false);
			double damage = attacker.AttackValue * (1 - target.Defence);
			target.HP -= damage;
			return (damage, target.HP <= 0);
		}

		public static void Heal(Entity target, double value) {
			target.HP += value;
			if (target.HP > target.BaseHP)
				target.HP = target.BaseHP;
		}

		public bool Buff(string param, double value) {
			switch (param) {
				case "hp":
					HP += value;
					break;
				case "atk":
					AttackValue += value;
					break;
				default:
					return false;
			}
			return true;
		}
	}

	class Hero : Entity {
		public Hero() : base("Герой", 500, 50, 0.5, 1, 0.5) { }
	}

	class Slime : Entity {
		public Slime() : base("Слайм", 100, 10, 0.1, 0.5, 0.0) { }
	}

	class WildBoar : Entity {
		public WildBoar() : base("Кабан", 200, 50, 0.3, 0.5, 0.3) { }
	}

	class Ogre : Entity {
		public Ogre() : base("Огр", 300, 100, 0.2, 0.7, 0.7) { }
	}
}
