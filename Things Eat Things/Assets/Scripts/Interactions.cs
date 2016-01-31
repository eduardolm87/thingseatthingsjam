﻿using UnityEngine;
using System.Collections;

public class Interactions : MonoBehaviour
{
    public enum Outcomes { Nothing, CanAttack, CanEmbodyFree };


    public static Outcomes GetOutcome(Creature.CREATURES playerType, Creature.CREATURES otherType)
    {
        Outcomes outcome = Outcomes.Nothing;

        switch (playerType)
        {
            case Creature.CREATURES.TinyLight:
                if (otherType == Creature.CREATURES.Rabbit)
                {
                    outcome = Outcomes.CanEmbodyFree;
                }
                break;

            case Creature.CREATURES.Wolf:
                switch (otherType)
                {
                    case Creature.CREATURES.Rabbit:
                    case Creature.CREATURES.Hunter:
                        outcome = Outcomes.CanAttack;
                        break;
                }

                break;

            case Creature.CREATURES.Hunter:
                switch (otherType)
                {
                    case Creature.CREATURES.Rabbit:
                    case Creature.CREATURES.Wolf:
                    case Creature.CREATURES.Hunter:
                        outcome = Outcomes.CanAttack;
                        break;
                }

                break;
        }

        return outcome;
    }
}
