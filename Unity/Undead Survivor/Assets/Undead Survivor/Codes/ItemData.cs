using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Melee, Range, Glove, Shoe, Heal }
                        // 근접공격, 원거리
    [Header("Main Info")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    [TextArea]  // 인스펙터에 여러줄 넣을수있게 속성 부여
    public string itemDesc; //아이템 설명
    public Sprite itemIcon; //아이템의 이미지

    [Header("Level Data")]
    // 레벨 0일때의 데이터
    public float baseDamage;
    public int baseCount;
    // 레벨 1이상일때의 데이터
    public float[] damages;
    public int[] counts;

    [Header("Weapon")]
    public GameObject projectile;   //발사체
    public Sprite hand; // 장착무기
}
