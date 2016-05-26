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
            Token pf35301 = new Token("3299527134-kMVohuPpsZ4IEO858OjCROT9LhtzdqFmi5MCd6P", "zs7vzSSHNOsdIxajpvYZSNMxtHxif5jpSrAaS3LeamTsi");
            var tokens = new[] {
                CoreTweet.Tokens.Create(matomeNaver.CK,matomeNaver.CS,pf35301.token, pf35301.tokenSecret)
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
