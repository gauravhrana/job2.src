/******************************************************************************
**		Name: ReleaseLogDetails
*******************************************************************************/

--EXEC dbo.ReleaseLogDetailsUpdate @ReleaseLogDetailsId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
--EXEC dbo.ReleaseLogDetailsUpdate @ReleaseLogDetailsId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
--EXEC dbo.ReleaseLogDetailsUpdate @ReleaseLogDetailsId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

EXEC dbo.ReleaseLogDetailInsert
		@ReleaseLogDetailId				=	-5011	
	,	@ApplicationId					=	100
	,	@ReleaseLogId					=	-5011						
	,	@ItemNo					        =	1						
	,	@Description					=	'Tested debugged and resolved Issues TTT-513, 510. 512, 509'
	,	@SortOrder						=	1
	,	@RequestedBy					=	'admin'
	,	@PrimaryDeveloper				=	'admin'
	,	@RequestedDate					=	16092012
	,	@AuditId						=	10

EXEC dbo.ReleaseLogDetailInsert
		@ReleaseLogDetailId				=	-5012	
	,	@ApplicationId					=	100
	,	@ReleaseLogId					=	-5011						
	,	@ItemNo					        =	2						
	,	@Description					=	'Implemented VerifyApplicationId value script that verifies one record'
	,	@SortOrder						=	1
	,	@RequestedBy					=	'admin'
	,	@PrimaryDeveloper				=	'admin'
	,	@RequestedDate					=	16092012
	,	@AuditId						=	10

EXEC dbo.ReleaseLogDetailInsert
		@ReleaseLogDetailId				=	-5013	
	,	@ApplicationId					=	100
	,	@ReleaseLogId					=	-5011						
	,	@ItemNo					        =	3						
	,	@Description					=	'Resolved Stored Procedure Log issue and fixed Search SPs'
	,	@SortOrder						=	1
	,	@RequestedBy					=	'admin'
	,	@PrimaryDeveloper				=	'admin'
	,	@RequestedDate					=	16092012
	,	@AuditId						=	10

EXEC dbo.ReleaseLogDetailInsert
		@ReleaseLogDetailId				=	-5014	
	,	@ApplicationId					=	100
	,	@ReleaseLogId					=	-5011						
	,	@ItemNo					        =	4						
	,	@Description					=	'Tested debugged and resolved Issues TTT-527, 528, 529, 530, 531'
	,	@SortOrder						=	1
	,	@RequestedBy					=	'admin'
	,	@PrimaryDeveloper				=	'admin'
	,	@RequestedDate					=	16092012
	,	@AuditId						=	10

EXEC dbo.ReleaseLogDetailInsert
		@ReleaseLogDetailId				=	-5015	
	,	@ApplicationId					=	100
	,	@ReleaseLogId					=	-5011						
	,	@ItemNo					        =	5					
	,	@Description					=	'Created shell procedure for AuditHistoryLastValues'
	,	@SortOrder						=	1
	,	@RequestedBy					=	'admin'
	,	@PrimaryDeveloper				=	'admin'
	,	@RequestedDate					=	16092012
	,	@AuditId						=	10

EXEC dbo.ReleaseLogDetailInsert
		@ReleaseLogDetailId				=	-5016	
	,	@ApplicationId					=	100
	,	@ReleaseLogId					=	-5011						
	,	@ItemNo					        =	6						
	,	@Description					=	'Implemented VerifyApplicationIdValue script that parses all tables'
	,	@SortOrder						=	1
	,	@RequestedBy					=	'admin'
	,	@PrimaryDeveloper				=	'admin'
	,	@RequestedDate					=	16092012
	,	@AuditId						=	10

EXEC dbo.ReleaseLogDetailInsert
		@ReleaseLogDetailId				=	-5017	
	,	@ApplicationId					=	100
	,	@ReleaseLogId					=	-5011						
	,	@ItemNo					        =	7					
	,	@Description					=	'Resolved issues 409 and updated SystemEntityTypeGetNextSequence and updated all associated scripts'
	,	@SortOrder						=	1
	,	@RequestedBy					=	'admin'
	,	@PrimaryDeveloper				=	'admin'
	,	@RequestedDate					=	16092012
	,	@AuditId						=	10

EXEC dbo.ReleaseLogDetailInsert
		@ReleaseLogDetailId				=	-5018	
	,	@ApplicationId					=	100
	,	@ReleaseLogId					=	-5011						
	,	@ItemNo					        =	8						
	,	@Description					=	'Resolved exceptions from TTT rebase'
	,	@SortOrder						=	1
	,	@RequestedBy					=	'admin'
	,	@PrimaryDeveloper				=	'admin'
	,	@RequestedDate					=	16092012
	,	@AuditId						=	10



EXEC dbo.ReleaseLogDetailInsert
		@ReleaseLogDetailId				=	-5019	
	,	@ApplicationId					=	100
	,	@ReleaseLogId					=	-5012						
	,	@ItemNo					        =	1						
	,	@Description					=	'Involve in Technical discussion on Architecture of TTT'
	,	@SortOrder						=	1
	,	@RequestedBy					=	'admin'
	,	@PrimaryDeveloper				=	'admin'
	,	@RequestedDate					=	16092012
	,	@AuditId						=	10

EXEC dbo.ReleaseLogDetailInsert
		@ReleaseLogDetailId				=	-5020	
	,	@ApplicationId					=	100
	,	@ReleaseLogId					=	-5012						
	,	@ItemNo					        =	2					
	,	@Description					=	'Review Technical discussion of TTT and added additional doc'
	,	@SortOrder						=	1
	,	@RequestedBy					=	'admin'
	,	@PrimaryDeveloper				=	'admin'
	,	@RequestedDate					=	16092012
	,	@AuditId						=	10




EXEC dbo.ReleaseLogDetailInsert
		@ReleaseLogDetailId				=	-5021	
	,	@ApplicationId					=	100
	,	@ReleaseLogId					=	-5013						
	,	@ItemNo					        =	1						
	,	@Description					=	'Check CRUD application'
	,	@SortOrder						=	1
	,	@RequestedBy					=	'admin'
	,	@PrimaryDeveloper				=	'admin'
	,	@RequestedDate					=	16092012
	,	@AuditId						=	10
