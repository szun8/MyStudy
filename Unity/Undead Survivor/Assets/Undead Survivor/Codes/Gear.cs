using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    // 장갑은 플레이어의 공격속도, 신발은 이동속도를 증가시킬 것
    public ItemData.ItemType type;
    // 장비의 타입과 수치를 저장할 변수 선언
    public float rate; // 레벨별 데이터

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Gear " + data.itemId;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        // Property Set
        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();    // 한번 생성될 때 능력 업!  
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();    // 레벨업될 때도 능력 업!!
    }

    void ApplyGear()
    {   // 타입에 따라 적절하게 로직을 적용시켜주는 함수
        switch (type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }

    void RateUp()
    {   // 장갑의 기능인 플레이어가 가진 모든 무기에 대한 연사력을 올리는 함수
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach(Weapon weapon in weapons)
        {
            switch (weapon.id)
            {
                case 0:     // 근접무기
                    weapon.speed = 150 + (150 * rate);
                    break;
                default:    // 원거리 무기
                    weapon.speed = 0.5f * (1f - rate);  // 0.5는 1초에 두 번... 값이 작아질수록 엄청 나게 빠르게 발사됨
                    break;
            }
        }
    }

    void SpeedUp()
    {   // 신발의 기능인 플레이어의 이동속도를 올리는 함수
        float speed = 3f;   //기본속도
        GameManager.instance.player.speed = speed + (speed * rate);
    }

}
