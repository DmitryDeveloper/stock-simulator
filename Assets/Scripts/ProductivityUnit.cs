using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductivityUnit : Unit
{
    private ResourcePile m_CurrentPile = null;
    public float ProductivityMultiplier = 2;

    protected override void BuildingInRange()
    {
        if (m_CurrentPile == null)
        {
            //The notation “as ResourcePile” sets the pile variable to m_Target only if m_Target is a ResourcePile type
            //This is an efficient way of checking whether m_Target is a resource pile. If it is (pile != null), 
            //then m_CurrentPile is set to that resource pile, and its ProductionSpeed is doubled.
            ResourcePile pile = m_Target as ResourcePile;

            if (pile != null)
            {
                m_CurrentPile = pile;
                m_CurrentPile.ProductionSpeed *=  ProductivityMultiplier;
            }
        }
    }

    void ResetProductivity()
    {
        if (m_CurrentPile != null)
        {
            m_CurrentPile.ProductionSpeed /= ProductivityMultiplier;
            m_CurrentPile = null;
        }
    }

    //rewrite both GoTo methods to reset productivity when user left target
    public override void GoTo(Building target)
    {
        ResetProductivity();
        base.GoTo(target); // run method from base class
    }

    public override void GoTo(Vector3 position)
    {
        ResetProductivity();
        base.GoTo(position);
    }
}
