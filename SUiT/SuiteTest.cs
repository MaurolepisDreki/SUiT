// Essential Suite Test Module

namespace Unit.SelfTest {
	class SuiteTest {
		// Test Runner
		public static void Run() {
			SuiteTest test = new SuiteTest();

			test.Run_Setup_ONLY();
			Loader.Assert( test._setup );
			Loader.Assert( ! test._clean );
			test.Reset();

			test.Run_Clean_ONLY();
			Loader.Assert( ! test._setup );
			Loader.Assert( test._clean );
			test.Reset();

			test.Run_Both();
			Loader.Assert( test._setup );
			Loader.Assert( test._clean );
			test.Reset();

			test.Run_None();
			Loader.Assert( ! test._setup );
			Loader.Assert( ! test._clean );
//			delete test;
		}

		// Variables for noting that the function was ran
		private bool _setup;
		private bool _clean;
		private Unit.Suite tstsut;

		public SuiteTest() {
			Reset();
			tstsut = new Unit.Suite( "SuiteTest" );
		}

		public void Reset() {
			_setup = _clean = false;
		}

		public void CB_Setup() {
			_setup = true;
		}

		public void CB_Clean() {
			_clean = true;
		}

		public void Run_Setup_ONLY() {
			tstsut.SetSetup( CB_Setup );
			tstsut.SetCleanup( null );
			tstsut.Run();
		}

		public void Run_Clean_ONLY() {
			tstsut.SetSetup( null );
			tstsut.SetCleanup( CB_Clean );
			tstsut.Run();
		}

		public void Run_Both() {
			tstsut.SetSetup( CB_Setup );
			tstsut.SetCleanup( CB_Clean );
			tstsut.Run();
		}

		public void Run_None() {
			tstsut.SetSetup( null );
			tstsut.SetCleanup( null );
			tstsut.Run();
		}
	}
}

