using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main.Template;
using System.Linq;

namespace Main.Common
{
    /// <summary>
    /// ヒトデのリーダー
    /// </summary>
    public class SeastarLeader : MonoBehaviour, IMainGameManager, ISeastarLeader
    {
        /// <summary>準委任帳票</summary>
        private Dictionary<EnumQuasiAssignmentForm, string>[] _quasiAssignForm;
        /// <summary>ステージIDキャッシュ</summary>
        private int _stageID = -1;

        public bool SetAssignState(EnumSeastarID enumSeastarID, bool assignState)
        {
            try
            {
                if (_stageID < 0)
                    _stageID = GetStageId();
                foreach (var item in _quasiAssignForm.Where(q => q[EnumQuasiAssignmentForm.MainSceneStagesModulesStateIndex].Equals($"{_stageID}") &&
                    q[EnumQuasiAssignmentForm.SeastarID].Equals($"{enumSeastarID}") &&
                    q[EnumQuasiAssignmentForm.AssignedDefault].Equals(ConstGeneric.DIGITFORM_FALSE)))
                {
                    item[EnumQuasiAssignmentForm.Assigned] = assignState ? ConstGeneric.DIGITFORM_TRUE : ConstGeneric.DIGITFORM_FALSE;
                }


                return true;
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                return false;
            }
        }

        public bool IsAssigned(EnumSeastarID enumSeastarID)
        {
            if (_stageID < 0)
                _stageID = GetStageId();
            if (0 < _quasiAssignForm.Where(q => q[EnumQuasiAssignmentForm.MainSceneStagesModulesStateIndex].Equals($"{_stageID}") &&
                q[EnumQuasiAssignmentForm.SeastarID].Equals($"{enumSeastarID}") &&
                q[EnumQuasiAssignmentForm.AssignedDefault].Equals(ConstGeneric.DIGITFORM_TRUE))
                .ToArray()
                .Length)
                return true;

            return false;
        }

        private int GetStageId()
        {
            // ステージIDの取得
            var currentStageDic = MainGameManager.Instance.SceneOwner.GetSystemCommonCash();
            return currentStageDic[EnumSystemCommonCash.SceneId];
        }

        public void OnStart()
        {
            // 準委任帳票の取得
            var tResourcesAccessory = new MainTemplateResourcesAccessory();
            var quasiAssignFormResources = tResourcesAccessory.LoadSaveDatasCSV(ConstResorcesNames.QUASI_ASSIGNMENT_FORM);
            _quasiAssignForm = tResourcesAccessory.GetQuasiAssignmentForm(quasiAssignFormResources);
        }

        public bool SaveAssigned()
        {
            try
            {
                var tResourcesAccessory = new MainTemplateResourcesAccessory();
                if (!tResourcesAccessory.SaveDatasCSVOfQuasiAssignmentForm(ConstResorcesNames.QUASI_ASSIGNMENT_FORM, _quasiAssignForm))
                    throw new System.Exception("準委任帳票をCSVデータへ保存呼び出しの失敗");

                return true;
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                return false;
            }
        }

        public int GetAssinedCounter()
        {
            try
            {
                // 準委任帳票の取得
                var tResourcesAccessory = new MainTemplateResourcesAccessory();
                var quasiAssignFormResources = tResourcesAccessory.LoadSaveDatasCSV(ConstResorcesNames.QUASI_ASSIGNMENT_FORM);
                var quasiAssignForm = tResourcesAccessory.GetQuasiAssignmentForm(quasiAssignFormResources);

                return quasiAssignForm.Where(q => q[EnumQuasiAssignmentForm.Assigned].Equals(ConstGeneric.DIGITFORM_TRUE))
                    .Select(q => q).ToArray()
                    .Length;
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                return -1;
            }
        }
    }

    /// <summary>
    /// ヒトデのリーダー
    /// </summary>
    public interface ISeastarLeader
    {
        /// <summary>
        /// アサインされているか
        /// </summary>
        /// <param name="enumSeastarID">ヒトデの識別ID</param>
        /// <returns>アサイン済み／未アサイン</returns>
        public bool IsAssigned(EnumSeastarID enumSeastarID);

        /// <summary>
        /// アサイン
        /// </summary>
        /// <param name="enumSeastarID">ヒトデの識別ID</param>
        /// <param name="assignState">アサイン状態</param>
        /// <returns>成功／失敗</returns>
        public bool SetAssignState(EnumSeastarID enumSeastarID, bool assignState);

        /// <summary>
        /// アサインを保存
        /// </summary>
        /// <returns>成功／失敗</returns>
        public bool SaveAssigned();

        /// <summary>
        /// アサイン人数を取得
        /// </summary>
        /// <returns>人数</returns>
        public int GetAssinedCounter();
    }
}
