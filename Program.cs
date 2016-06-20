//post
//lib:CoreTweet
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using CoreTweet;
using CoreTweet.Streaming;

namespace tweet
{
    class Program
    {
        static void Main(string[] args)
        {
            Consumer _consumer = new Consumer("rLiy8r1ChLkjm58RmrJtsAuWg", "yby8MpKk95L2PCRm8hV6ZSYseYy6paPQRBl3PD28TpukgREVFo");
            Token _token = new Token("3299527134-kMVohuPpsZ4IEO858OjCROT9LhtzdqFmi5MCd6P", "zs7vzSSHNOsdIxajpvYZSNMxtHxif5jpSrAaS3LeamTsi");
            Account account = new Account(_consumer, _token);

            //account.HomeTimeLine();
            //account.UserTimeLine();
            //account.follow("");
            //account.tweet(Console.ReadLine());
            //account.userFav("rinaty0514",100);
            account.streamingFav("ErlangRunner");

            Console.ReadKey();
        }
    }

    class Consumer
    {
        public string CK { get; private set; }
        public string CS { get; private set; }

        public Consumer(string _ck, string _cs)
        {
            CK = _ck;
            CS = _cs;
        }
    }

    class Token
    {
        public string accessToken { get; private set; }
        public string tokenSecret { get; private set; }

        public Token(string _accessToken, string _tokenSecret)
        {
            accessToken = _accessToken;
            tokenSecret = _tokenSecret;
        }
    }

    class Account
    {
        public string accessToken { get; private set; }
        public string tokenSecret { get; private set; }
        public string CK { get; private set; }
        public string CS { get; private set; }
        Tokens account;

        public Account(Consumer _consumer, Token _token)
        {
            accessToken = _token.accessToken;
            tokenSecret = _token.tokenSecret;
            CK = _consumer.CK;
            CS = _consumer.CS;
            account = Tokens.Create(CK, CS, accessToken, tokenSecret);
        }

        public void tweet(string tweetText)
        {
            account.Statuses.Update(new { status = tweetText });
            Console.WriteLine("-----------------終了-----------------");
        }

        public void HomeTimeLine()
        {
            Console.WriteLine("HomeTimeline");
            foreach (CoreTweet.Status status in account.Statuses.HomeTimeline(count => 5000))
            {
                //Console.WriteLine(status.GetType().Name);
                Console.WriteLine("User:" + status.User.ScreenName);
                Console.WriteLine(status.Text);
                Console.WriteLine("--------------------------------");
            }
            Console.WriteLine("-----------------終了-----------------");
        }

        public void UserTimeLine()
        {
            Console.WriteLine("UserTimeline");
            foreach (CoreTweet.Status status in account.Statuses.UserTimeline(count => 5000))
            {
                //Console.WriteLine(status.GetType().Name);
                Console.WriteLine("User:" + status.User.ScreenName);
                Console.WriteLine(status.Text);
                Console.WriteLine("--------------------------------");
            }
            Console.WriteLine("-----------------終了-----------------");
        }

        public void follow(string target)
        {
            try
            {
                account.Friendships.Create(screen_name => target);
                Console.WriteLine($"{target}:Followed");
            }
            catch
            {
                Console.WriteLine("Follow Failed");
            }
            Console.WriteLine("-----------------終了-----------------");
        }

        public void userFav(string userID, int _count)
        {
            Console.WriteLine($"UserID:{userID}");
            foreach (CoreTweet.Status status in account.Statuses.UserTimeline(screen_name => userID, count => _count))
            {
                try
                {
                    account.Favorites.Create(id => status.Id);
                    Console.WriteLine(status.Text);
                    Console.WriteLine("---------------------------");
                }
                catch
                {
                    Console.WriteLine("Fav Failed");
                }
            }
            Console.WriteLine("-----------------終了-----------------");
        }

        public void streamingFav(string userID)
        {
            Action<Status> fav = (state =>
            {
                if (userID == state.User.ScreenName)
                {
                    account.Favorites.Create(id => state.Id);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"User:{state.User.ScreenName}\n{state.Text}\nFaved\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine($"User:{state.User.ScreenName}\n{state.Text}\n");
                }
            }
            );

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"TargetUser:{userID}\n");
            Console.ForegroundColor = ConsoleColor.White;
            account.Streaming.UserAsObservable()
                .Where((StreamingMessage m) => m.Type == MessageType.Create)
                .Cast<StatusMessage>()
                .Select((StatusMessage m) => m.Status)
                .Subscribe((Status s) => fav(s),
                (Exception ex) => Console.WriteLine(ex),
                () => Console.WriteLine("えらー"));

            //Thread.Sleep(TimeSpan.FromSeconds(10));

            //disposable.Dispose();

        }
    }
}
