//
// Unit tests for DispatchGroup
//
// Authors:
//	Marek Safar (marek.safar@gmail.com)
//
// Copyright 2013 Xamarin Inc. All rights reserved.
//

using System;
using System.Net;

#if XAMCORE_2_0
using Foundation;
using CoreFoundation;
#else
using MonoTouch.CoreFoundation;
using MonoTouch.Foundation;
#endif
using NUnit.Framework;

namespace MonoTouchFixtures.CoreFoundation {
	
	[TestFixture]
	[Preserve (AllMembers = true)]
	public class DispatchGroupTest
	{
		[Test]
		public void WaitTest ()
		{
			using (var dg = DispatchGroup.Create ()) {
				var dq = DispatchQueue.GetGlobalQueue (DispatchQueuePriority.Default);
			
				dg.DispatchAsync (dq, delegate {
					Console.WriteLine ("Inside dispatch");
				});
			
				Assert.IsTrue (dg.Wait (DispatchTime.Forever));
				dq.Dispose ();
			}
		}

		[Test]
		public void EnterLeaveTest ()
		{
			using (var dg = DispatchGroup.Create ()) {
				dg.Enter ();
				Assert.IsFalse (dg.Wait (new DispatchTime (1000 * 1000 * 1000)), "#1");
				dg.Leave ();
				Assert.IsTrue (dg.Wait (DispatchTime.Forever), "#2");
			}
		}

	}
}
