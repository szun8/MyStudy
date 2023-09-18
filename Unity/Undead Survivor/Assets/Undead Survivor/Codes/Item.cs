using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;   // 무기 오브젝트와 연동을 위한 변수 생성
    public Gear gear;       // 장비 오브젝트와 연동을 위한 변수 생성

    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1]; // 배열로 부모 panel 이미지도 가져오기에 부모 제외한 첫번째 아이콘 이미지 가져오기 위해 인덱싱
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>(); // 텍스트로 레벨, 이름, 설명을 받아야하기에 배열로 요소 Get
        textLevel = texts[0];
        textName = texts[1];
        textName.text = data.itemName;  // 한번 뜨면 바뀌지 않을 이름은 바로 초기화
        textDesc = texts[2];
    }

    private void OnEnable()
    {   // 오브젝트가 활성화되었을때 실행되는 함수
        textLevel.text = "Lv." + (level + 1);

        switch (data.itemType)  // 아이템 타입에 따른 매개변수 갯수차이로 스위치문 설정
        {   // 무기에 대해서는 들어가야할 값 2개
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                textDesc.text = string.Format(data.itemDesc, data.damages[level]*100, data.counts[level]);
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                textDesc.text = string.Format(data.itemDesc, data.damages[level]*100);
                break;
            default:
                textDesc.text = string.Format(data.itemDesc);
                break;
        }
    }

    public void OnClick()
    {
        switch (data.itemType)
        {   // 버튼을 클릭하면 그에 대한 기능로직이 구동되며 해당 버튼에 대한 레벨을 상승시킴
            // 무기에 대해서는 하나의 로직으로 코드를 작성해줌
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                if(level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                }
                else
                {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damages[level];    // 백분율로 데미지 설정을 해놔서 기본 * 얼마나 높일지
                    nextCount += data.counts[level];

                    weapon.LevelUp(nextDamage, nextCount);  // 진짜 능력업 로직 진행
                }
                level++;
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if(level == 0)
                {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                }
                else
                {
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);
                }
                level++;
                break;
            case ItemData.ItemType.Heal:
                GameManager.instance.health = GameManager.instance.maxHealth;
                break;
        }
        if(level == data.damages.Length)
        {   // 최대 레벨(현재 +5까지 설정해놓음)에 달성하면 버튼의 활성화를 해제
            GetComponent<Button>().interactable = false;
        }
    }
}
