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
            Consumer _consumer = new Consumer("", "");
            Token _token = new Token("", "");
            Account account = new Account(_consumer, _token);

            account.HomeTimeLine();
            account.UserTimeLine();
            account.tweet("test");
            Console.ReadKey();
        }
    }

    class Consumer {
        public string CK { get; private set; }
        public string CS { get; private set; }

        public Consumer(string _ck,string _cs) {
            CK = _ck;
            CS = _cs;
        }
    }

    class Token {
        public string accessToken { get; private set; }
        public string tokenSecret { get; private set; }

        public Token(string _accessToken,string _tokenSecret) {
            accessToken = _accessToken;
            tokenSecret = _tokenSecret;
        }
    }

    class Account {
        public string accessToken { get; private set; }
        public string tokenSecret { get; private set; }
        public string CK { get; private set; }
        public string CS { get; private set; }
        public string limitID;
        CoreTweet.Tokens account;

        public Account(Consumer _consumer,Token _token) {
            accessToken = _token.accessToken;
            tokenSecret = _token.tokenSecret;
            CK = _consumer.CK;
            CS = _consumer.CS;
            account = CoreTweet.Tokens.Create(CK,CS,accessToken,tokenSecret);
        }

        public void tweet(string tweetText) {
            account.Statuses.Update(new{status = tweetText});
        }

        public void HomeTimeLine() {
            foreach(CoreTweet.Status status in account.Statuses.HomeTimeline(count => 5000)) {
                //Console.WriteLine(status.GetType().Name);
                Console.WriteLine("HomeTimeline");
                if(status.User.ScreenName == limitID || limitID == null) {
                    Console.WriteLine("User:" + status.User.ScreenName);
                    Console.WriteLine(status.Text);
                    Console.WriteLine("--------------------------------");
                }
            }
        }

        public void UserTimeLine() {
            foreach(CoreTweet.Status status in account.Statuses.UserTimeline(count => 5000)) {
                //Console.WriteLine(status.GetType().Name);
                Console.WriteLine("UserTimeline");
                if(status.User.ScreenName == limitID || limitID == null) {
                    Console.WriteLine("User:" + status.User.ScreenName);
                    Console.WriteLine(status.Text);
                    Console.WriteLine("--------------------------------");
                }
            }
        }
    }
}
