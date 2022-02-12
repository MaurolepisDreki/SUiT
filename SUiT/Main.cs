/* SharpUnit SelfTest Loader */

using System;
using System.Runtime.CompilerServices;

// CMD   I:  THOU SHALT NOT "using Unit;"!  
// CMD  II:  THOU SHALT NOT "using System.Diagnostics;"!
// CMD III:  THOU SHALT USE "Loader.Assert" FOR ALL ASSERTATIONS!
// CMD  IV:  THOU SHALT USE ONLY static TEST METHODS FROM THE Loader!
// CMD   V:  THOU SHALT WRAP ALL SAID TEST METHODS USING RunWrapper!
// CMD  VI:  THOU SHALT NOT EVER, UNDER ANY CIRCUMSTANCE, OR FOR ANY REASON, 
//             SANE OR OTHERWISE, CREATE A TEST THAT WRITES OUTPUT TO THE SHELL,
//             LEAST YOU SMITE ALL THE OUTPUT WITH A CURSE LASTING TO THE END OF YOUR GENERATIONS!
// CMD VII:  IN THE EVENT YOU HAVE BROKEN ANY ONE OF THESE CMDs, THOU SHALT CUT
//             DOWN A TREE WITH A HERING THAT THOU MIGHT BE FORGIVEN THY TRESSPASS!
//             MAYBE!  JUST MAYBE!  IF THE MOOD STRIKES ME, THAT IS!

namespace Unit.SelfTest {
	// Constants for System.Environment.Exit
	//   C# could learn a thing or two from C (including how to dynamically cast enums to ints)
	public struct EXIT {
		public  const  int  SUCCESS  =  0;
		public  const  int  FAILURE  =  1;
	}
	
	class Loader {
		const string _indent = @"   ";

		// Assertations are ALWAYS Fatal
		//   They are when the program assumes a truth that proves false when checked.
		//   E.g. xmalloc( size ) { tmp = malloc( size ); Assert( tmp != null ); return tmp; }
		//   Many conisider this do-or-die mentality bad, and perhapse it is overused, but it is essential.
		public static void Assert( bool expr, 
				[CallerFilePath] string where = "",
				[CallerLineNumber] int when = 0,
				[CallerMemberName] string who = "",
				[CallerArgumentExpression( "expr" )] string what = "" ) {
			/*********************************************************************/
			if( ! expr ) {
				ConsoleColor cc = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write( $" ASSERT: {who} [{where}:{when}]: " );
				Console.ForegroundColor = cc;
				Console.WriteLine( what );
				System.Environment.Exit( EXIT.FAILURE );
			}
		}
				
		// Do Run-Length Decode on string
		//   NOTE: callFile & callLine are included exclusivly for correctly 
		//      identifying the cause of our assertation failure.  (No guessing, Human!)
		public static string RunLengthDecoder( string str, int len = 1, 
				[CallerFilePath] string callFile = "", [CallerLineNumber] int callLine = 0 ) {
			/*********************************************************************/
			// Validate Input
			Assert( len >= 0, callFile, callLine );

			// Declare Variables
			string ret = String.Empty;

			// Short Circuit Result
			if( str.Length != 0 && len != 0 ) {
				if( len == 1 ) return str;

				// Expand String
				for( int i = 0; i < len; i++ )
					ret += str;
			}

			// Deliver Result
			return ret;
		}

		// Application Heading
		private static void Print_Heading() {
			Console.WriteLine();
			Console.WriteLine( $"{{0}}{Unit.LibInfo.NAME}  {Unit.LibInfo.VERSION.STRING}",
				RunLengthDecoder( _indent, 2 ) );
			Console.WriteLine( $"{{0}}Self-Test Loader \"SUiT\"",
				RunLengthDecoder( _indent, 3 ) );
			Console.WriteLine( $"{{0}} \u00A9MMXXII  Maurolepis Dreki",
				RunLengthDecoder( _indent, 2 ) );
			Console.WriteLine();
		}

		// Wraps the/a Test Runner in output text so the user is not left hanging
		public static void RunWrapper( string msg, Action cb ) {
			ConsoleColor cc;

			// Print Message and Run Callback
			Console.WriteLine( $"{msg}..." );
			cb();

			// Get Current Console Data
			cc = Console.ForegroundColor;

			// Append "OK" to message in Green
			Console.CursorTop--;
			Console.Write( $"{msg}... " );
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine( "OK" );

			// Reset Console Data
			Console.ForegroundColor = cc;
		}

		// Loader
		private static void Main () {
			Print_Heading();
			
			RunWrapper( "Testing Engine Setup/Clean[up]", EngineTest.Run );
			RunWrapper( "Testing Suite Setup/Clean[up]", SuiteTest.Run );
			RunWrapper( "Testing Test Setup/Test/Clean[up]", TestTest.Run );
		}
	}
}

