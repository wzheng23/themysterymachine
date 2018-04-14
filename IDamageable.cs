using UnityEngine;

public interface IDamageable {

    void Damage(int damage, Vector3 hitPoint);

    int Health();

    int MaxHealth();

    int Threshold();

    void SetNextThreshold(int i);

    void Dead();
			
}
