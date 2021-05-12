using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyShootingAI
{
    EnemyShootingObject MakeShooter(GameObject target);
}
