//post
//lib:CoreTweet
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace post {
    class Program {
        static void Main(string[] args) {
            Consumer matomeNaver = new Consumer("rLiy8r1ChLkjm58RmrJtsAuWg", "yby8MpKk95L2PCRm8hV6ZSYseYy6paPQRBl3PD28TpukgREVFo");
            Token _Token = new Token("", "");
            var tokens = new[] {
                CoreTweet.Tokens.Create(matomeNaver.CK,matomeNaver.CS,_Token.token, _Token.tokenSecret)
            };
            
            string text = "test";
            foreach(var _tokens in tokens) {
                _tokens.Statuses.Update(new { status = text});
            }
            Console.ReadKey();
        }
    }

    class Consumer {
        public string CK { get; private set; }
        public string CS { get; private set; }

        public Consumer(string ck,string cs) {
            CK = ck;
            CS = cs;
        }
    }

    class Token {
        public string token { get; private set; }
        public string tokenSecret { get; private set; }

        public Token(string _token,string _tokenSecret) {
            token = _token;
            tokenSecret = _tokenSecret;
        }
    }
}
