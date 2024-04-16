using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaiterAni : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //激活施法后续

    public void Active1()
    {
        TimerManager.instance.Invoke(GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length, delegate
        {
            //鞠躬
            GetComponent<Animator>().SetTrigger("Bow");
            TimerManager.instance.Invoke(1.67f, delegate { transform.parent.GetComponent<Waiter1>().TransState(transform.parent.GetComponent<Waiter1>(), ActorStateType.Transit); });

        });
    }
    //激活放菜后续
    public void Active2()
    {
        Customer1 customer = MessageSend.instance.combine[transform.parent.GetComponent<Waiter1>()];


        //激活桌上的菜品
        //ActiveOnDesk(customer);
        //播放施法动画
        TimerManager.instance.Invoke(StaticVar.waiteConjure, delegate
        {
            transform.parent.GetComponent<Waiter1>().PlayAnim("Conjure");
            customer.PlayAnim("Expect");
            //客人期待过后切换感谢状态
            TimerManager.instance.Invoke(StaticVar.expectTime, delegate
            { customer.TransState(customer, ActorStateType.Thanks); });
        });
    }
    //激活菜品
    public void ActiveOnDesk(Customer1 customer)
    {
        GameObject OnDeskItem = Instantiate(Resources.Load("Prefabs/Items/" + customer._loveOrder) as GameObject);
        //判断楼层
        if (customer.currentFloor == 1)
        {
            OnDeskItem.transform.SetParent(GameObject.Find("FirstFloorItems").transform);
        }
        else
        {
            OnDeskItem.transform.SetParent(GameObject.Find("SecondFloorItems").transform);
        }

        OnDeskItem.transform.localPosition = StaticVar.VecTranslate(MessageSend.instance.customerSeat[customer].parent.Find("OnDeskItem"));
        OnDeskItem.SetActive(true);
        //OnDeskItem.name = customer._loveOrder;
    }
}
