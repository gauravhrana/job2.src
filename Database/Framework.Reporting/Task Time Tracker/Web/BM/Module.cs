//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using DataModel.Framework.Core;
//using Framework.Components.Core;

//namespace ApplicationContainer.UI.Web.BM
//{
//	public class Module
//	{
//		public static void Initalize()
//		{
//			//SetupClientRoute();
//			SetupBMRoutes();
//		}

//		private static void SetupBMRoutes()
//		{
//			List<string> entityNamesBM = new List<string>(new string[]	{
//				"Competency","CompetencyXSkill","Skill","SkillLevel","SkillXPersonXSkillLevel",
//				"Client", "ClientXProject", "Layer", "Milestone",
//				"MilestoneFeatureState", "MilestoneXFeature", "MilestoneXFeatureArchive",
//				"Project", "ProjectTimeLine","ProductivityArea","ProductivityAreaFeature",
//				"ProductivityAreaFeatureXApplicationUser","ProductivityAreaXProductivityAreaFeature",
//				"Question","QuestionCategory","Feature","FeatureGroup", "FeatureGroupXFeature",
//				"FeatureRule", "FeatureRuleCategory", "FeatureRuleStatus",
//				"FeatureXFeatureRule", "Need","NeedXFeature", "ProjectUseCaseStatus", "ProjectUseCaseStatusArchieve",
//				"ProjectXNeed", "ProjectXUseCase", "UseCase", "UseCaseActor", "UseCaseActorXUseCase", 
//				"UseCasePackage", "UseCasePackageXUseCase", "UseCaseRelationship", "UseCaseStep",
//				"UseCaseWorkFlowCategory", "UseCaseXUseCaseStep","Reward","Risk",
//				"CustomTimeLog","Schedule",	"ScheduleDetail","ScheduleHistory","ScheduleItem", 
//				"ScheduleQuestion","ScheduleState", "VacationPlan","ScheduleView",
//				 "TestCase", "TestCaseOwner", "TestCasePriority", "TestCaseStatus",
//				"TestRun", "TestSuite", "TestSuiteXTestCase",				
//				"TestSuiteXTestCaseArchive","ProjectPortfolio", "ProjectPortfolioGroup",
//				"ProjectPortfolioGroupXProjectPortfolio","Activity","ActivityState",	"ActivityXDeliverableArtifact",
//				"DeliverableArtifact","DeliverableArtifactStatus", "FeatureXTask","Task", "TaskAlgorithm","TaskAlgorithmItem","TaskFormulation",
//				"TaskNote","TaskPackage","TaskPackageXOwnerXTask","TaskPersonMapping","TaskPriorityType",
//				"TaskPriorityXApplicationUser","TaskTiskRewardRankingPerson","TaskRole","TaskStatusType","TaskType","TaskXActivityInstance",
//				"TaskXCompetency","TaskXDeliverableArtifact"
//			});

//			foreach (string entity in entityNamesBM)
//			{
//				var oApplicationRouteDataModel = new ApplicationRouteDataModel();

//				oApplicationRouteDataModel.EntityName = entity;
//				var route1 = string.Empty;

//				var items = ApplicationRouteDataManager.GetEntityDetails(oApplicationRouteDataModel, Global.GetStartupProfile());
//				switch (entity)
//				{
//					case "Client":
//					case "ClientXProject":
//					case "Layer":
//					case "Milestone":
//						route1 = "~/BM/PMO/";
//						break;

//					case "MilestoneFeatureState":
//					case "MilestoneXFeature":
//					case "MilestoneXFeatureArchive":
//						route1 = "~/BM/PMO/Milestone/";
//						break;

//					case "Project":
//					case "ProjectTimeLine":
//						route1 = "~/BM/PMO/ProjectManagement/";
//						break;

//					case "ProjectPortfolio":
//					case "ProjectPortfolioGroup":
//					case "ProjectPortfolioGroupXProjectPortfolio":
//						route1 = "~/BM/PMO/ProjectManagement/Project/";
//						break;

//					case "FeatureGroup":
//					case "FeatureGroupXFeature":
//					case "FeatureRule":
//					case "FeatureRuleCategory":
//					case "FeatureRuleStatus":
//					case "FeatureXFeatureRule":
//						route1 = "~/BM/RequirementAnalysis/Feature/";
//						break;
//					case  "Need":
//					case  "NeedXFeature":
//					case  "ProjectUseCaseStatus":
//					case  "ProjectUseCaseStatusArchieve":
//					case  "ProjectXNeed":
//					case  "ProjectXUseCase":
//					case  "UseCase":
//					case  "UseCaseActor":
//					case  "UseCaseActorXUseCase":
//					case  "UseCasePackage":
//					case  "UseCasePackageXUseCase":
//					case  "UseCaseRelationship":
//					case  "UseCaseStep":
//					case  "UseCaseWorkFlowCategory":
//					case  "UseCaseXUseCaseStep":
//						route1 = "~/BM/RequirementAnalysis/";
//						break;
//					case "DeliverableArtifactStatus":
//						route1 = "~/BM/WBS/DeliverableArtifact/";
//						break;

//					case "TestCase":
//					case "TestCaseOwner":
//					case "TestCasePriority":
//					case "TestCaseStatus":
//					case "TestRun":
//					case "TestSuite":
//					case "TestSuiteXTestCase":
//					case "TestSuiteXTestCaseArchive":
//						route1 = "~/BM/TCM/";
//						break;
//					case "Reward":
//					case "Risk":
//						route1 = "~/BM/RiskAndReward/";
//						break;
//					case "CustomTimeLog":
//					case "Schedule":
//					case "ScheduleDetail":
//					case "ScheduleHistory":
//					case "ScheduleItem":
//					case "ScheduleQuestion":
//					case "ScheduleState":
//					case "VacationPlan":
//					case "ScheduleView":
//						route1 = "~/BM/Scheduling/";
//						break;
//					case "ProductivityArea":
//					case "ProductivityAreaFeature":
//					case "ProductivityAreaFeatureXApplicationUser":
//					case "ProductivityAreaXProductivityAreaFeature":
//						route1 = "~/BM/Productivity/";
//						break;
//					case "Competency":
//					case "CompetencyXSkill":
//					case "Skill":
//					case "SkillLevel":
//					case "SkillXPersonXSkillLevel":
//						route1 = "~/BM/Aptitude/";
//						break;
//					case "Activity":
//					case "ActivityState":
//					case "ActivityXDeliverableArtifact":
//					case "DeliverableArtifact":
//					case "FeatureXTask":
//					case "Task":
//					case "TaskAlgorithm":
//					case "TaskAlgorithmItem":
//					case "TaskFormulation":
//					case "TaskNote":
//					case "TaskPackage":
//					case "TaskPackageXOwnerXTask":
//					case "TaskPersonMapping":
//					case "TaskPriorityType":
//					case "TaskPriorityXApplicationUser":
//					case "TaskTiskRewardRankingPerson":
//					case "TaskRole":
//					case "TaskStatusType":
//					case "TaskType":
//					case "TaskXActivityInstance":
//					case "TaskXCompetency":
//					case "TaskXDeliverableArtifact":
//						route1 = "~/BM/WBS/";
//						break;
//					case "Question":
//						route1 = "~/BM/";
//						break;
//					case "QuestionCategory":
//						route1 = "~/BM/Question/";
//						break;	
//				}

//				foreach (var dataItem in items)
//				{
//					if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRoute"))
//					{
//						if (route1 != string.Empty)
//							dataItem.RelativeRoute = string.Format(route1 + "{0}/{{action}}.aspx", dataItem.EntityName);
//						else
//							dataItem.RelativeRoute = string.Format("~/BM/{0}/{{action}}.aspx", dataItem.EntityName);
//					}
//					else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSuperKey"))
//					{
//						if (route1 != string.Empty)
//							dataItem.RelativeRoute = string.Format(route1 + "{0}/{{action}}.aspx", dataItem.EntityName);
//						else
//							dataItem.RelativeRoute = string.Format("~/BM/{0}/{{action}}.aspx", dataItem.EntityName);
//					}
//					else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSearch"))
//					{
//						if (route1 != string.Empty)
//							dataItem.RelativeRoute = string.Format(route1 + "{0}/Default.aspx", dataItem.EntityName);
//						else
//							dataItem.RelativeRoute = string.Format("~/BM/{0}/Default.aspx", dataItem.EntityName);
//					}

//					ApplicationRouteDataManager.Update(dataItem, Global.GetStartupProfile());
//				}
//			}
//		}
		
//	}
//}