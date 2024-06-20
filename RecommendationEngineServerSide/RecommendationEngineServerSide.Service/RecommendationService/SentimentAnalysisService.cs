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
            { "undercooked", -0.8 }
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

