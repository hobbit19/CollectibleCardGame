﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Card
{
    public enum AbilityType
    {
        NO_ABILITY,
        INSTANT_ACTIVE,
        DOUBLE_ATTACK,
        SHIELD,
        PROVOCATION,
        REGENERATION_EACH_TURN,
        COUNTER_ATTACK
    }

    public enum SpellType
    {
        NO_SPELL,
        HEAL_ALLY_FIELD_CARDS,
        DAMAGE_ENEMY_FIELD_CARDS,
        HEAL_ALLY_HERO,
        DAMAGE_ENEMY_HERO,
        HEAL_ALLY_CARD,
        DAMAGE_ENEMY_CARD,
        SHIELD_ON_ALLY_CARD,
        PROVOCATION_ON_ALLY_CARD,
        BUFF_CARD_DAMAGE,
        DEBUFF_CARD_DAMAGE
    }

    public enum TargetType
    {
        NO_TARGET,
        ALLY_CARD_TARGET,
        ENEMY_CARD_TARGET
    }

    public string Name;
    public Sprite Logo;
    public int Attack, Defense, Manacost;
    public bool CanAttack;
    public bool IsPlaced;

    public List<AbilityType> Abilities;
    public SpellType Spell;
    public TargetType SpellTarget;
    public int SpellValue;

    public bool IsAlive
    {
        get
        {
            return Defense > 0;
        }
    }

    public bool IsSpell
    {
        get
        {
            return Spell != SpellType.NO_SPELL;
        }
    }
    public bool HasAbility
    {
        get
        {
            return Abilities.Count > 0;
        }
    }
    public bool IsProvocation
    {
        get
        {
            return Abilities.Exists(x => x == AbilityType.PROVOCATION);
        }
    }

    public int TimesDealedDamage;

    public Card(string name, string logoPath, int attack, int defense, int manacost,
                AbilityType abilityType = 0, SpellType spellType = 0, int spellVal = 0,
                TargetType targetType = 0)
    {
        Name = name;
        Logo = Resources.Load<Sprite>(logoPath);
        Attack = attack;
        Defense = defense;
        Manacost = manacost;
        CanAttack = false;
        IsPlaced = false;

        Abilities = new List<AbilityType>();

        if (abilityType != 0)
            Abilities.Add(abilityType);

        Spell = spellType;
        SpellTarget = targetType;
        SpellValue = spellVal;

        TimesDealedDamage = 0;
    }

    public void GetDamage(int dmg)
    {
        if (dmg > 0)
        {
            if (Abilities.Exists(x => x == AbilityType.SHIELD))
                Abilities.Remove(AbilityType.SHIELD);
            else
                Defense -= dmg;
        }
    }

    public Card GetCopy()
    {
        Card card = this;
        card.Abilities = new List<AbilityType>(Abilities);
        return card;
    }
}

public static class CardManager
{
    public static List<Card> AllCards = new List<Card>();
}

public class CardManagerScr : MonoBehaviour
{
    public void Awake()
    {
        CardManager.AllCards.Add(new Card("Assasin", "Sprites/Cards/Assasin", 5, 5, 6));
        CardManager.AllCards.Add(new Card("Samurai", "Sprites/Cards/Samurai", 4, 3, 5));
        CardManager.AllCards.Add(new Card("Ronin", "Sprites/Cards/Ronin", 3, 3, 4));
        CardManager.AllCards.Add(new Card("Warrior", "Sprites/Cards/Warrior", 2, 1, 2));
        CardManager.AllCards.Add(new Card("Valkyrie", "Sprites/Cards/Valkyrie", 8, 1, 7));
        CardManager.AllCards.Add(new Card("Viking", "Sprites/Cards/Viking", 1, 1, 1));

        CardManager.AllCards.Add(new Card("provocation", "Sprites/Cards/provocation", 1, 2, 3, Card.AbilityType.PROVOCATION));
        CardManager.AllCards.Add(new Card("regeneration", "Sprites/Cards/regen", 4, 2, 5, Card.AbilityType.REGENERATION_EACH_TURN));
        CardManager.AllCards.Add(new Card("doubleAttack", "Sprites/Cards/doubleAttack", 3, 2, 4, Card.AbilityType.DOUBLE_ATTACK));
        CardManager.AllCards.Add(new Card("instantActive", "Sprites/Cards/instantActive", 2, 1, 2, Card.AbilityType.INSTANT_ACTIVE));
        CardManager.AllCards.Add(new Card("shield", "Sprites/Cards/shield", 5, 1, 7, Card.AbilityType.SHIELD));
        CardManager.AllCards.Add(new Card("counterAttack", "Sprites/Cards/counterAttack", 3, 1, 1, Card.AbilityType.COUNTER_ATTACK));

        CardManager.AllCards.Add(new Card("HEAL_ALLY_FIELD_CARDS", "Sprites/Cards/healAllyCards", 0, 0, 2, 0, 
            Card.SpellType.HEAL_ALLY_FIELD_CARDS, 2, Card.TargetType.NO_TARGET));
        CardManager.AllCards.Add(new Card("DAMAGE_ENEMY_FIELD_CARDS", "Sprites/Cards/damageEnemyCards", 0, 0, 2, 0, 
            Card.SpellType.DAMAGE_ENEMY_FIELD_CARDS, 2, Card.TargetType.NO_TARGET));
        CardManager.AllCards.Add(new Card("HEAL_ALLY_HERO", "Sprites/Cards/healAllyHero", 0, 0, 2, 0, 
            Card.SpellType.HEAL_ALLY_HERO, 2, Card.TargetType.NO_TARGET));
        CardManager.AllCards.Add(new Card("DAMAGE_ENEMY_HERO", "Sprites/Cards/damageEnemyHero", 0, 0, 2, 0, 
            Card.SpellType.DAMAGE_ENEMY_HERO, 2, Card.TargetType.NO_TARGET));
        CardManager.AllCards.Add(new Card("HEAL_ALLY_CARD", "Sprites/Cards/healAllyCard", 0, 0, 2, 0, 
            Card.SpellType.HEAL_ALLY_CARD, 2, Card.TargetType.ALLY_CARD_TARGET));
        CardManager.AllCards.Add(new Card("DAMAGE_ENEMY_CARD", "Sprites/Cards/damageEnemyCard", 0, 0, 2, 0, 
            Card.SpellType.DAMAGE_ENEMY_CARD, 2, Card.TargetType.ENEMY_CARD_TARGET));
        CardManager.AllCards.Add(new Card("SHIELD_ON_ALLY_CARD", "Sprites/Cards/shieldOnAllyCard", 0, 0, 2, 0, 
            Card.SpellType.SHIELD_ON_ALLY_CARD, 0, Card.TargetType.ALLY_CARD_TARGET));
        CardManager.AllCards.Add(new Card("PROVOCATION_ON_ALLY_CARD", "Sprites/Cards/provocationOnAllyCard", 0, 0, 2, 0, 
            Card.SpellType.PROVOCATION_ON_ALLY_CARD, 0, Card.TargetType.ALLY_CARD_TARGET));
        CardManager.AllCards.Add(new Card("BUFF_CARD_DAMAGE", "Sprites/Cards/buffCardDamage", 0, 0, 2, 0, 
            Card.SpellType.BUFF_CARD_DAMAGE, 2, Card.TargetType.ALLY_CARD_TARGET));
        CardManager.AllCards.Add(new Card("DEBUFF_CARD_DAMAGE", "Sprites/Cards/debuffCardDamage", 0, 0, 2, 0, 
            Card.SpellType.DEBUFF_CARD_DAMAGE, 2, Card.TargetType.ENEMY_CARD_TARGET));
    }
}