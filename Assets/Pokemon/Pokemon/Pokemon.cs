using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pokemon : PokemonBase
{
    public IEnumerator Roar()
    {
        if (player) yield return null;

        Image render = GetComponentInChildren<Image>();

        render.sprite = data.front2;

        yield return new WaitForSeconds(0.5f);

        render.sprite = data.frontSprite;

        yield return new WaitForSeconds(0.35f);

        BattleManager.instance.EnableAnim();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!player) { ScreenTransition.done += CallRoar; }
    }

    public void CallRoar()
    {
        StartCoroutine(Roar());
        ScreenTransition.done -= CallRoar;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
