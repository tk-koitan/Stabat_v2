using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using KoitanLib;
using Koitan;

public class CursorHand : MonoBehaviour
{
    [SerializeField]
    float cursorVelocity = 100f;
    //[SerializeField]
    //Transform kawacoinTrans;
    [SerializeField]
    Chip chip;
    [SerializeField]
    GameObject Kawaztan;

    //�v���p�e�B�ɑ������p
    [SerializeField]
    int id;


    Rigidbody2D rigidbody2D;
    CircleCollider2D circleCollider2D;

    public bool Havecoin { get; private set; }
    public int ID { get; private set; }

    public enum PlayerKind
    {
        None,
        Human,
        Computer
    }

    public PlayerKind playerKind;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        Havecoin = false;
        ID = id;

        playerKind = PlayerKind.None;
    }

    // Update is called once per frame
    void Update()
    {
        //�K��
        switch (playerKind) {
            case PlayerKind.None:
                Kawaztan.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                break;
            case PlayerKind.Human:
                Kawaztan.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                break;
            case PlayerKind.Computer:
                Kawaztan.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                break;
        }


        PutChip();

        Move();

        if (KoitanInput.GetDown(ButtonCode.Y, ID))
        {
            CursorHand curtmp = IsKawaztanCollision();
            if (curtmp != null) curtmp.ChangePlayerKind();
            else ChangePlayerKind();
        }

        BattleCheck();
    }
    void PutChip()
    {
        //A�����Ă��͈͓��Ƀ`�b�v������Ƃ�
        if ((KoitanInput.GetDown(ButtonCode.A, ID) && IsCollision()))
        {
            //�`�b�v�����u��
            Havecoin = !Havecoin;
            //�J�[�\���̈ړ����~�߂�
            rigidbody2D.velocity = Vector2.zero;
        }
        else if (KoitanInput.GetDown(ButtonCode.B, ID))
        {
            //B�������Ƃ��`�b�v����
            Havecoin = true;
        }
        chip.hadCoin = Havecoin;
    }

    void Move()
    {
        //�ړ����͂ő��x������
        rigidbody2D.velocity += cursorVelocity * KoitanInput.GetStick(ID) * Time.deltaTime;

        //�R�C�������Ă锻��̂Ƃ�
        if (Havecoin)
        {
            //�w��Ƀ`�b�v�̎��ꏊ���ړ�����D
            Vector3 ofs = circleCollider2D.offset;
            chip.gameObject.transform.DOMove(transform.position + ofs , 0.05f);
        }
    }

    public void ChangePlayerKind()
    {
        playerKind++;
        playerKind = (PlayerKind)((int)playerKind % ((int)PlayerKind.Computer + 1));
        Koitan.BattleSetting.ControllPlayers[ID] = (int)playerKind;
    }

    void BattleCheck()
    {
        if (KoitanInput.GetDown(ButtonCode.X, ID) && Chip.CanStartBattle())
        {
            Chip.DeleteChipsList();
            BattleManager.StartBattle();
        }
    }

    //�����o�Ŏ����Ă�`�b�v�Ɠ���ID�̂Ƃ�true
    bool IsCollision()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, circleCollider2D.radius);

        foreach (Collider2D col in collisions)
        {
            if (col.tag == "Chip" &&
                (col.GetComponent<Chip>().CursorHand.ID == ID /*�܂��̓R���s���[�^*/ ||
                (col.GetComponent<Chip>().CursorHand.playerKind == PlayerKind.Computer && !col.GetComponent<Chip>().CursorHand.Havecoin)))
            {
                //����chip���擾����
                chip = col.GetComponent<Chip>();
                return true;
            }
        }
        return false;
    }

    CursorHand IsKawaztanCollision()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, circleCollider2D.radius);

        foreach (Collider2D col in collisions)
        {
            if (col.tag == "Player")
            {
                return col.GetComponent<SelectedCharacter>().CursorHand;
            }
        }
        return null;
    }
}
