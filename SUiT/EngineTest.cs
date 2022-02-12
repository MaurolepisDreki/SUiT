// Essential Engine Test Module

namespace Unit.SelfTest {
	class EngineTest {
		// Test Runner
		public static void Run() {
			EngineTest test = new EngineTest();

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
		private Unit.Engine tsteng;

		public EngineTest() {
			Reset();
			tsteng = new Unit.Engine();
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
			tsteng.SetSetup( CB_Setup );
			tsteng.SetCleanup( null );
			tsteng.Run();
		}

		public void Run_Clean_ONLY() {
			tsteng.SetSetup( null );
			tsteng.SetCleanup( CB_Clean );
			tsteng.Run();
		}

		public void Run_Both() {
			tsteng.SetSetup( CB_Setup );
			tsteng.SetCleanup( CB_Clean );
			tsteng.Run();
		}

		public void Run_None() {
			tsteng.SetSetup( null );
			tsteng.SetCleanup( null );
			tsteng.Run();
		}
	}
}

