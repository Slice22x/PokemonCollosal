using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BattleUIAnimator : MonoBehaviour
{
    //Normal pos
    [SerializeField] RectTransform pStatPos,eStatPos,fightPos,pokePos,itemPos,runPos;

    //Objects
    [SerializeField] RectTransform pStat, eStat, fight, run, pokemon, item;

    [SerializeField] RectTransform buttons, selection, description, holder;

    List<Vector3> positions = new List<Vector3>();

    public bool active { get; private set; }

    public delegate void UIDone();
    public static UIDone doneTweening;

    public static BattleUIAnimator instance;

    private void Awake()
    {
        instance = this;
        active = true;
    }

    public void EnterDesc(bool fight)
    {
        //Set the scale
        if (fight)
        {
            selection.sizeDelta = new Vector3(550, 160);
            selection.localPosition = new Vector3(-125, -145);
            description.gameObject.SetActive(true);
            description.sizeDelta = new Vector2(250, 160);
            description.localPosition = new Vector3(275, -145);
        }
        if (!fight)
        {
            selection.sizeDelta = new Vector3(800, 160);
            selection.localPosition = new Vector3(0, -145);
            description.gameObject.SetActive(false);
        }

        buttons.DOLocalMove(new Vector3(buttons.localPosition.x, 40), 1f).SetEase(Ease.OutCubic);
        holder.DOLocalMove(new Vector3(holder.localPosition.x, 0), 1f).SetEase(Ease.OutCubic);
    }

    public void EnterUI()
    {
        //Sets the in positions
        if(positions.Count == 0)
        {
            positions.Add(pStat.position);
            positions.Add(eStat.position);
            positions.Add(fight.position);
            positions.Add(pokemon.position);
            positions.Add(item.position);
            positions.Add(run.position);
            positions.Add(holder.position);
        }

        pStat.DOMove(pStatPos.position, 1f).SetEase(Ease.OutCubic).OnComplete(() => { eStat.DOMove(eStatPos.position, 1f).SetEase(Ease.OutCubic).OnComplete(() => { doneTweening?.Invoke(); active = true; }); });

        fight.DOMove(fightPos.position, 0.5f).SetEase(Ease.OutCirc);
        pokemon.DOMove(pokePos.position, 0.5f).SetEase(Ease.OutCirc).SetDelay(0.375f);
        item.DOMove(itemPos.position, 0.5f).SetEase(Ease.OutCirc).SetDelay(0.75f);
        run.DOMove(runPos.position, 0.5f).SetEase(Ease.OutCirc).SetDelay(1.125f);
    }

    public void ExitUISlow(System.Action act)
    {
        pStat.DOMove(positions[0], 1f).SetEase(Ease.OutCubic).OnComplete(() => { eStat.DOMove(positions[1], 1f).SetEase(Ease.OutCubic).OnComplete(() => act.Invoke() ); });

        fight.DOMove(positions[2], 0.5f).SetEase(Ease.OutCirc);
        pokemon.DOMove(positions[3], 0.5f).SetEase(Ease.OutCirc).SetDelay(0.375f);
        item.DOMove(positions[4], 0.5f).SetEase(Ease.OutCirc).SetDelay(0.75f);
        run.DOMove(positions[5], 0.5f).SetEase(Ease.OutCirc).SetDelay(1.125f);
        holder.DOMove(positions[6], 0.5f).SetEase(Ease.OutCirc);
        active = false;
    }


    public IEnumerator ExitUIFast(System.Action act)
    {
        pStat.DOMove(positions[0], 1f).SetEase(Ease.OutCubic);

        eStat.DOMove(positions[1], 1f).SetEase(Ease.OutCubic);

        fight.DOMove(positions[2], 0.5f).SetEase(Ease.OutCirc);
        pokemon.DOMove(positions[3], 0.5f).SetEase(Ease.OutCirc);
        item.DOMove(positions[4], 0.5f).SetEase(Ease.OutCirc);
        run.DOMove(positions[5], 0.5f).SetEase(Ease.OutCirc);
        holder.DOMove(positions[6], 0.5f).SetEase(Ease.OutCirc);
        buttons.DOLocalMove(new Vector3(200, -116), 0.5f).SetEase(Ease.OutCirc);

        yield return new WaitForSeconds(1.1f);

        active = false;

        act.Invoke();
    }
}
