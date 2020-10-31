using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OOP_Laba9 {
	static class Game {
		private static Hero Hero { get; set; }

		private static Random rndm = new Random();

		static Game() {
			Attack += Entity.Attacked;
			Heal += Entity.Heal;
		}

		private delegate (double, bool) AttackHandler(Entity target, Entity attacker);
		private static event AttackHandler Attack;

		private delegate void HealHandler(Entity target, double value);
		private static event HealHandler Heal;

		public static void Play() {
			Hero = new Hero();
			Console.Clear();
			while (Hero.HP > 0) {
				Console.Write(
					"1 - в бой" +
					"\n2 - посмотреть статистику героя" +
					"\n0 - завершить игру" +
					"\nВыберите действие: "
					);
				if (!int.TryParse(Console.ReadLine(), out int choice1)) {
					Console.WriteLine("Нет такого действия");
					continue;
				}
				switch (choice1) {
					case 1: {
						Console.Clear();
						List<Entity> monsters = new List<Entity>();
						for (int i = 0; i < 3; i++) {
							Entity monster = (rndm.Next(0, 3)) switch
							{
								0 => new Slime(),
								1 => new WildBoar(),
								2 => new Ogre(),
								_ => throw new Exception(),
							};
							monsters.Add(monster);
						}
						while (monsters.Count > 0 && Hero.HP > 0) {
							Console.Write("Hero: ");
							if (Hero.HP < Hero.BaseHP * 0.33)
								Console.ForegroundColor = ConsoleColor.Red;
							else if (Hero.HP > Hero.BaseHP * 0.66)
								Console.ForegroundColor = ConsoleColor.Green;
							else
								Console.ForegroundColor = ConsoleColor.Yellow;
							Console.Write(Hero.HP);
							Console.ForegroundColor = ConsoleColor.Gray;
							Console.WriteLine($" / {Hero.BaseHP} hp");
							int counter = 0;
							foreach (var monster in monsters) {
								Console.Write($"{++counter}) {monster.Name,-15}: ");
								if (monster.HP < monster.BaseHP * 0.33)
									Console.ForegroundColor = ConsoleColor.Red;
								else if (monster.HP > monster.BaseHP * 0.66)
									Console.ForegroundColor = ConsoleColor.Green;
								else
									Console.ForegroundColor = ConsoleColor.Yellow;
								Console.Write($"{monster.HP, 0:0}");
								Console.ForegroundColor = ConsoleColor.Gray;
								Console.WriteLine($" / {monster.BaseHP} hp");
							}

							Console.Write(
								"Введите номер врага для атаки или введите h для лечения" +
								"\nВыберите действие: "
								);
							string choice2 = Console.ReadLine();
							if (choice2 == "h")
								Heal(Hero, 100);
							else if (!int.TryParse(choice2, out int monsterN) || monsterN < 1 || monsterN > monsters.Count) {
								Console.WriteLine("Нет такого действия");
								Console.ReadLine();
								Console.Clear();
								continue;
							}
							else {
								var result = Attack(monsters[monsterN - 1], Hero);
								Console.WriteLine($"Нанесённый урон: {result.Item1, 0:0}");
								if (result.Item2) {
									Console.WriteLine("Монстр убит");
									monsters.RemoveAt(monsterN - 1);
								}
							}
							
							foreach (var monster in monsters) {
								var result = Attack(Hero, monster);
								Console.WriteLine($"{monster.Name} ударил на {result.Item1, 0:0}");
							}
							Console.ReadKey();
							Console.Clear();
						}
						if (Hero.HP <= 0)
							break;
						else
							Heal(Hero, 1000);
						Hero.Buff("hp", 100);
						Hero.Buff("atk", 50);
					}
					break;
					case 2:
						Console.WriteLine(
							$"Hp: {Hero.HP}" +
							$"\nAttack: {Hero.AttackValue}"
							);
						break;
					default:
						throw new Exception();
				}
				Console.WriteLine("Нажмите кнопку для продолжения");
				Console.ReadKey();
				Console.Clear();
			}
			Console.WriteLine("\nУмер мужик");
			Console.ReadKey();
			Console.Clear();
		}
	}
}
