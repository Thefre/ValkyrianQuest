using System;
using System.Collections;
using System.Collections.Generic;

public class Character {
	protected DateTime readyTime;

	public delegate void DeathAction();
	public event DeathAction OnDeath;
	public delegate void ReadyAction();
	public event ReadyAction OnReady;

	private float _hp = 100;
	private float _maxHP = 100;
	private float _mp = 100;
	private float _maxMP = 100;

	public string charName = "Character";
	public string hurtAnim = "Hurt";

	public float strength = 10;
	public float dexterity = 10;
	public float magic = 10;
	public float endurance = 10;
	public float agility = 10;
	public float spirit = 10;
	public float presence = 10;
	public float willpower = 10;
	public float charisma = 10;

	public float attackPower = 10;
	public float magicPower = 10;
	public float slashDefense = 10;
	public float bluntDefense = 10;
	public float pierceDefense = 10;
	public float fireDefense = 10;
	public float waterDefense = 10;
	public float airDefense = 10;
	public float earthDefense = 10;
	public float accuracy = 10;
	public float evade = 10;
	public float mass = 10;
	public float size = 10;

	
	public Character target;

	public List<AbilityData> abilityList = new List<AbilityData>();
	public AbilityData[] activeAbilities = new AbilityData[4];
	public AbilityData[] defenseAbilities = new AbilityData[4];

	public bool isAlive = true;
	public bool isReady = false;
	public bool isPlayer = false;
	
	public float HP {
		get {
			return (float)Math.Floor(_hp);
		}
		set {
			_hp = value;
			if (_hp < 1) {
				_hp = 0;
				if(OnDeath != null) //Trigger OnDeath
					OnDeath();
				isAlive = false;
			} else if (_hp > _maxHP) {
				_hp = _maxHP;
			}
		}
	}
	public float maxHP {
		get {
			return (float)Math.Floor(_maxHP);
		}
		set {
			float ratio = HPratio;
			_maxHP = value;
			if (_maxHP < 1) {
				_maxHP = 1;
			}
			if (_hp > _maxHP) {
				_hp = _maxHP;
			} else {
				_hp = _maxHP * ratio;
			}
			
		}
	}
	public float HPratio {
		get {
			return _hp / _maxHP;
		}
	}
	public float MP {
		get {
			return (float)Math.Floor(_mp);
		}
		set {
			_mp = value;
			if (_mp < 1) {
				_mp = 0;
			} else if (_mp > _maxMP) {
				_mp = _maxMP;
			}
		}
	}
	public float maxMP {
		get {
			return (float)Math.Floor(_maxMP);
		}
		set {
			float ratio = MPratio;
			_maxMP = value;
			if (_maxMP < 1) {
				_maxMP = 1;
			}
			if (_mp > _maxMP) {
				_mp = _maxMP;
			} else {
				_mp = _maxMP * ratio;
			}
			
		}
	}
	public float MPratio {
		get {
			return _mp / _maxMP;
		}
	}

	public virtual void OnStart() {
		readyTime = DateTime.UtcNow + TimeSpan.FromSeconds(3);
	}

	public virtual void CheckReady() {
		if (!isReady) {
			if (DateTime.UtcNow >= readyTime) {
				isReady = true;
				if(OnReady != null) //Trigger OnReady
					OnReady();
			}
		}
	}

	public void DoSkill(int skillID) {
		switch(skillID) {
		case 0:
			SkillA();
			break;
		case 1:
			SkillB();
			break;
		}
	}

	protected void ClassAbility(string name, string actionID, TimeSpan delay, TimeSpan cooldown, string desc = "No description available.", int level = 1, int spCost = 0, int hpCost = 0) {
		AbilityData newAbility = new AbilityData();
		newAbility.name = name;
		newAbility.actionID = actionID;
		newAbility.description = desc;
		newAbility.delay = delay;
		newAbility.coolDown = cooldown;
		newAbility.level = level;
		newAbility.spCost = spCost;
		newAbility.hpCost = hpCost;
		abilityList.Add(newAbility);
	}

	public virtual void SkillA() {
		isReady = false;
		readyTime = DateTime.UtcNow + TimeSpan.FromSeconds(2);
	}
	public virtual void SkillB() {
		isReady = false;
		readyTime = DateTime.UtcNow + TimeSpan.FromSeconds(2);
	}
	public virtual void SkillC() {
		isReady = false;
		readyTime = DateTime.UtcNow + TimeSpan.FromSeconds(2);
	}
	public virtual void SkillD() {
		isReady = false;
		readyTime = DateTime.UtcNow + TimeSpan.FromSeconds(2);
	}
}
