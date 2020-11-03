using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class PlaymodeTest
    {
        private SceneStimuli scriptStimuli;
        private Vector3 scale = new Vector3(1, 1, 1);

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
            yield return new EnterPlayMode();
        }

        [UnityTest]
        public IEnumerator IsVehicle_Scale_Valid()
        {

            GameObject stimuli = GameObject.Find("TestStimuli");
            scriptStimuli = stimuli.GetComponent<SceneStimuli>();

            Vector3 initialLossyScale = scriptStimuli.GetLossyScale();
            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(scale, initialLossyScale);
            Object.Destroy(stimuli.gameObject);
        }

        [UnitySetUp]
        public IEnumerator TearDown()
        {

            yield return new ExitPlayMode();
        }
    }
}
