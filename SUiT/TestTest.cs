// Essential Test Test Module

// NOTE: Unlike Engine and Suite, Tests cannot run empty; they require a Test CB at construction which cannot be changed durring execution.

namespace Unit.SelfTest {
	class TestTest {
		// Test Runner
		public static void Run() {
			TestTest test = new TestTest();

			test.Run_SetupTest_ONLY();
			Loader.Assert( test._setup );
			Loader.Assert( test._test );
			Loader.Assert( ! test._clean );
			test.Reset();

			test.Run_CleanTest_ONLY();
			Loader.Assert( ! test._setup );
			Loader.Assert( test._test );
			Loader.Assert( test._clean );
			test.Reset();

			test.Run_All();
			Loader.Assert( test._setup );
			Loader.Assert( test._test );
			Loader.Assert( test._clean );
			test.Reset();

			test.Run_Test_ONLY();
			Loader.Assert( ! test._setup );
			Loader.Assert( test._test );
			Loader.Assert( ! test._clean );
//			delete test;
		}

		// Variables for noting that the function was ran
		private bool _setup;
		private bool _test;
		private bool _clean;

		public TestTest() {
			Reset();
		}

		public void Reset() {
			_setup = _test = _clean = false;
		}

		public void CB_Setup() {
			_setup = true;
		}

		public void CB_Test() {
			_test = true;
		}

		public void CB_Clean() {
			_clean = true;
		}

		public void Run_SetupTest_ONLY() {
			Unit.Test.New( null, CB_Test, CB_Setup ).Run();
		}

		public void Run_CleanTest_ONLY() {
			Unit.Test.New( null, CB_Test, null, CB_Clean ).Run();
		}

		public void Run_All() {
			Unit.Test.New( null, CB_Test, CB_Setup, CB_Clean ).Run();
		}

		public void Run_Test_ONLY() {
			Unit.Test.New( null, CB_Test ).Run();
		}
	}
}

