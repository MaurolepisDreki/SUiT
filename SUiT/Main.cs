/* SharpUnit SelfTest Loader */

using System;
using System.Runtime.CompilerServices;

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
		static void Assert( bool expr, 
				[CallerFilePath] string where = "",
				[CallerLineNumber] int when = 0,
				[CallerMemberName] string who = "",
				[CallerArgumentExpression( "expr" )] string what = "" ) {
			/*********************************************************************/
			if( ! expr ) {
				Console.WriteLine( $" ASSERT: {who} [{where}:{when}]: {what}" );
				System.Environment.Exit( EXIT.FAILURE );
			}
		}
				
		// Do Run-Length Decode on string
		//   NOTE: callFile & callLine are included exclusivly for correctly 
		//      identifying the cause of our assertation failure.  (No guessing, human!)
		static string RunLengthDecoder( string str, int len = 1, 
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

		static void Print_Heading() {
			Console.WriteLine();
			Console.WriteLine( $"{{0}}{Unit.LibInfo.NAME}",
				RunLengthDecoder( _indent, 2 ) );
			Console.WriteLine();
		}

		static void Main () {
			Print_Heading();
		}
	}
}

