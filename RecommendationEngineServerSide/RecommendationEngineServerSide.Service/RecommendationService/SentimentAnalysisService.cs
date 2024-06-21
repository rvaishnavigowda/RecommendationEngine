using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Service.RecommendationService
{
    public static class SentimentAnalysisService
    {

        private static readonly Dictionary<string, double> SentimentScores = new Dictionary<string, double>
{
    { "excellent", 2.0 },
    { "amazing", 1.5 },
    { "awesome", 1.5 },
    { "fantastic", 1.5 },
    { "great", 1.0 },
    { "good", 0.5 },
    { "very good", 0.8 },
    { "very tasty", 1.2 },
    { "tasty", 0.6 },
    { "fresh", 0.7 },
    { "crispy", 0.7 },
    { "not bad", 0.3 },
    { "bad", -0.5 },
    { "terrible", -1.0 },
    { "awful", -1.5 },
    { "horrible", -1.5 },
    { "hard", -0.8 },
    { "undercooked", -0.8 },
    { "enjoyable", 1.0 },
    { "pleasant", 1.0 },
    { "satisfying", 1.0 },
    { "nice", 1.0 },
    { "liked", 1.0 },
    { "delightful", 1.0 },
    { "pleasing", 1.0 },
    { "admirable", 1.0 },
    { "commendable", 1.0 },
    { "worthy", 1.0 },
    { "gratifying", 1.0 },
    { "pleasurable", 1.0 },
    { "appealing", 1.0 },
    { "lovely", 1.0 },
    { "congenial", 1.0 },
    { "agreeable", 1.0 },
    { "charming", 1.0 },
    { "rewarding", 1.0 },
    { "favorable", 1.0 },
    { "superior", 1.0 },
    { "praiseworthy", 1.0 },
    { "positive", 1.0 },
    { "encouraging", 1.0 },
    { "wonderful", 1.5 },
    { "love", 1.5 },
    { "exceptional", 1.5 },
    { "marvelous", 1.5 },
    { "brilliant", 1.5 },
    { "terrific", 1.5 },
    { "remarkable", 1.5 },
    { "phenomenal", 1.5 },
    { "extraordinary", 1.5 },
    { "magnificent", 1.5 },
    { "perfect", 1.5 },
    { "splendid", 1.5 },
    { "glorious", 1.5 },
    { "stellar", 1.5 },
    { "exquisite", 1.5 },
    { "superb", 1.5 },
    { "unmatched", 1.5 },
    { "unbeatable", 1.5 },
    { "impressive", 1.5 },
    { "stunning", 1.5 },
    { "sensational", 1.5 },
    { "divine", 1.5 },
    { "top-notch", 1.5 }
};


        public static double AnalyzeSentiment(string comment)
            {
                 if(comment!=null)
                 {
                      var words = comment.ToLower().Split(' ');
                      double score = 0;

                      foreach (var word in words)
                      {
                            if (SentimentScores.TryGetValue(word, out double wordScore))
                            {
                                 score += wordScore;
                            }
                      }

                      return score;
                 }
                else
            {
                return 0;
            }
            }
        }
    }

