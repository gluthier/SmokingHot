using UnityEngine;

public static class Env
{
    // Default values
    public static int DefaultMinHealth = 0;
    public static int DefaultHealth = 200;
    public static int DefaultMinMaxHealth = 100;
    public static int DefaultMaxHealth = 200;

    public static int DefaultMinStress = 0;
    public static int DefaultStress = 0;
    public static int DefaultMaxStress = 10;

    public static int DefaultMinCigAddiction = 0;
    public static int DefaultCigAddiction = 5;
    public static int DefaultMaxCigAddiction = 10;

    public static int DefaultMinAlcAddiction = 0;
    public static int DefaultAlcAddiction = 0;
    public static int DefaultMaxAlcAddiction = 10;

    public static int DefaultMinAttackSpeed = 5;
    public static int DefaultAttackSpeed = 10;
    public static int DefaultMaxAttackSpeed = 20;

    public static int DefaultMinAttackDamage = 5;
    public static int DefaultAttackDamage = 10;
    public static int DefaultMaxAttackDamage = 30;

    // Inventory
    public static int DefaultMinNumCigarette = 0;
    public static int DefaultNumCigarette = 20;
    public static int DefaultMaxNumCigarette = 200;

    public static int DefaultMinNumAlcool = 0;
    public static int DefaultNumAlcool = 0;
    public static int DefaultMaxNumAlcool = 20;

    // Runs
    public static int StartRunStressIncrement = 5;
    public static int PlayerIsHitStressIncrement = 1;

    public static int CigaretteStressReliever = 1;
    public static int CigaretteHealthReliever = 10;
    public static int CigaretteAttackSpeedIncrease = 1;
    public static int AlcoolStressReliever = 2;
    public static int AlcoolHealthReliever = 20;
    public static int AlcoolAttackDamageIncrease = 1;

    public static int EnterRoomAutomaticCigaretteTrigger = 8;
    public static int EnterRoomAutomaticAlcoolTrigger = 8;

    public static int ExitRoomCigaretteAddictionDecrease = 1;
    public static int ExitRoomAlcoolAddictionDecrease = 1;
    public static int ExitRoomAttackSpeedIncrease = 1;
    public static int ExitRoomAttackDamageIncrease = 1;

    public static int EndRunHpLossCigaretteAddiction = 5;
    public static int EndRunCigaretteAddictionHpLossTriggerValue = 5;
    public static int EndRunHpLossAlcoolAddiction = 5;
    public static int EndRunAlcoolAddictionHpLossTriggerValue = 5;

    // Bullet
    public static string PlayerBulletPrefab = "PlayerBullet";
    public static string EnemyBulletPrefab = "EnemyBullet";
    public static string BulletHolder = "BulletHolder";
    public static float PlayerBulletVelocity = 10f;
    public static float EnemyBulletVelocity = 9f;
    public static float BulletLifetime = 10f;

    // Enemy
    public static string EnemyPrefab = "Enemy";
    public static string EnemyHolder = "EnemyHolder";
    public static int EnemyBaseDamage = 10;
    public static Vector3 DefaultEnemySpawnPosition = new Vector3(6, 1, 0);

    // Tags
    public static string TagPlayer = "Player";
    public static string TagLevel = "Level";
    public static string TagEnemy = "Enemy";
}
