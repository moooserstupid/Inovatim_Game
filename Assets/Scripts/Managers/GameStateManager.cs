
using TMPro;
using UI.FloatingText;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    
    
    public class GameStateManager : MonoBehaviour
    {
        
        public static GameStateManager Instance { get; private set; }
        [SerializeField] private AudioClip moneyEarn;
        [SerializeField] private AudioClip fail;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private TextMeshProUGUI textMesh;

        public int money = 0;

        public int succes = 10;
        public int damagedSucces = 5;

        public int[] successRewardList;
        public int[] successButDamagedRewardList;

        public int not_alive = 5;
        public int speedUp = 50;

        public int goal = 50;
        public TextMeshProUGUI targetAmount;


        private GameState state;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(Instance);
            }
        }

        private void Start()
        {
            targetAmount.text = goal.ToString();
            _audioSource = GetComponent<AudioSource>();

            state = GameState.PLAYING;
        }

        public void EarnMoney(bool damaged, Vector3 worldPosition, string address)
        {
            int reward = 0, damagedReward = 0;
            switch (address)
            {
                case "A":
                    reward = successRewardList[0];
                    damagedReward = successButDamagedRewardList[0];
                    break;
                case "B":
                    reward = successRewardList[1];
                    damagedReward = successButDamagedRewardList[1];
                    break;
                case "C":
                    reward = successRewardList[2];
                    damagedReward = successButDamagedRewardList[2];
                    break;

            }
            if (damaged == true)
            {
                money += damagedReward;
                FloatingTextManager.Instance.ShowText("+$" + damagedReward.ToString(), worldPosition + new Vector3(-5, 0, 0), Color.green);
            }
            else
            {
                money += reward;
                FloatingTextManager.Instance.ShowText("+$" + reward.ToString(), worldPosition + new Vector3(-5, 0, 0), Color.green);
            }

            if (money >= goal && state == GameState.PLAYING)
            {
                GameWon();
            }
            textMesh.text = money.ToString();
            Debug.Log(money);
            _audioSource.PlayOneShot(moneyEarn, 0.7F);
        }

        public void GameWon()
        {
            state = GameState.ENDED;
            Debug.Log("Won");
            if (SceneManager.GetActiveScene().buildIndex == 4) //4 Build'teki son rakam
            {
                SceneManager.LoadScene(1); //CreditsScene
            }
            else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void GameLost()
        {
            state = GameState.ENDED;
            Debug.Log("Lost");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoseMoney(string durum, Vector3 worldPosition)
        {
            //if (durum == "not_alive")
            //{
            //    money -= not_alive;
            //}
            //else if (durum == "speed")
            //{
            //    money -= speedUp;
            //}
            //if (durum == "wrongaddress")
            //{
            //    money -= not_alive;
            //    FloatingTextManager.Instance.ShowText("-$" + not_alive.ToString(), worldPosition, Color.red);
            //}
            if (money < 0 && state == GameState.PLAYING)
            {
                GameLost();
            }
            textMesh.text = money.ToString();
            Debug.Log(money);
            //_audioSource.PlayOneShot(fail, 0.5F);
        }
    }
}



//private bool damaged = false;