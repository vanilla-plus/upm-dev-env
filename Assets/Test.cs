using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using UnityEngine;

using Vanilla.UnityExtensions;

using static UnityEngine.Debug;

namespace Vanilla.Core
{

    public class Test : MonoBehaviour
    {

	    // ---------------------------------------------------------------------------------------------- Standard data

//	    [ReadOnly]
	    public int testEveryXFrames = 10;
	    
	    public Transform t;

	    public List<long> dirtyTicks = new List<long>();
	    public List<long> cleanTicks = new List<long>();

	    public long dirtyAv;
	    public long cleanAv;

	    // ---------------------------------------------------------------------------------------------- Test specific
	    
	    public IEnumerable<Transform> dirty = new List<Transform>();
	    public IEnumerable<Transform> clean = new List<Transform>();

	    public List<Transform> dirtyPerm = new List<Transform>();
	    public List<Transform> cleanPerm = new List<Transform>();
	    
	    // ---------------------------------------------------------------------------------------------------- Methods
        
//	    void OnEnable() => t = transform;


	    void Update()
        {
//	        if (Time.frameCount % testEveryXFrames != 0) return;
	        
			// ------------------------------------------------------------------------------------------ Dirty warm-up

//			var dirtyWarmup = t.GetAllChildren();
			
	        // -------------------------------------------------------------------------------------------- Dirty timed

//	        var dirtyTime = Stopwatch.StartNew();
//
//	        var dirty = t.GetAllChildren();
//	        
//            dirtyTime.Stop();

            // ------------------------------------------------------------------------------------------ Clean warm-up
            
//            var cleanWarmup = transform.GetChildList();

            // -------------------------------------------------------------------------------------------- Clean timed
            
            var cleanTime = Stopwatch.StartNew();

//            if (!Input.GetKeyDown(KeyCode.Space)) return;

            clean = transform.GetAllChildren();
            
            cleanTime.Stop();
            
            // -------------------------------------------------------------------------------------------- Dirty Tally
            
//            if (dirtyTicks.Count > 100)
//            {
//	            dirtyTicks.RemoveAt(0);
//            }
//
//            dirtyTicks.Add(dirtyTime.ElapsedTicks);
//
//            dirtyAv = dirtyTicks.Sum() / 100;
            
            // -------------------------------------------------------------------------------------------- Clean Tally

            if (cleanTicks.Count > 100)
            {
	            cleanTicks.RemoveAt(index: 0);
            }

            cleanTicks.Add(item: cleanTime.ElapsedTicks);

            cleanAv = cleanTicks.Sum() / 100;
//////
            if (!Input.GetKeyDown(key: KeyCode.Space)) return;
//////
//	        Log("hi");
/// 
////            dirtyPerm = dirty.ToList();
            cleanPerm = clean.ToList();
        }

        [ContextMenu(itemName: "Outside PlayMode")]
        public void OutsideOfPlaymodeTest()
        {
            
        }
    }

}