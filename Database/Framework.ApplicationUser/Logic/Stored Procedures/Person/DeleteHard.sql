IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PersonDeleteHard')
BEGIN
	PRINT 'Dropping Procedure PersonDeleteHard'
	DROP  Procedure PersonDeleteHard
END
GO

PRINT 'Creating Procedure PersonDeleteHard'
GO

/******************************************************************************
**		File: 
**		Name: PersonDeleteHard
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------							-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.PersonDeleteHard
(
		@KeyId 				INT					
	,	@KeyType			VARCHAR(50)			
	,	@AuditId			INT					
	,	@AuditDate			DATETIME	= NULL	
	,	@SystemEntityType	VARCHAR(50)	= 'Person'			
)
AS
BEGIN

	IF @KeyType = 'PersonId'
		BEGIN 	

			EXEC	dbo.SystemDevNumbersDeleteHard
					@KeyId		=	@KeyId		 
				,	@KeyType	=	'PersonId'	
				,	@AuditId	=	@AuditId
			
			EXEC	dbo.SkillXPersonXSkillLevelDeleteHard
					@KeyId		=	@KeyId 
				,	@KeyType	=	'PersonId'
				,	@AuditId	=	@AuditId

			EXEC	dbo.BatchFileDeleteHard 
					@KeyId		=	@KeyId 
				,	@KeyType	=	'PersonId'
				,	@AuditId	=	@AuditId

			EXEC	dbo.ScheduleItemDeleteHard 
					@KeyId		=	@KeyId
				,	@KeyType	=	'PersonId'
				,	@AuditId	=	@AuditId

			EXEC	dbo.TaskRiskRewardRankingXPersonDeleteHard 
					@KeyId		=	@KeyId
				,	@KeyType	=	'PersonId'
				,	@AuditId	=	@AuditId

			EXEC	dbo.TaskPriorityXPersonDeleteHard 
					@KeyId		=	@KeyId
				,	@KeyType	=	'PersonId'
				,	@AuditId	=	@AuditId
	
			DELETE	dbo.Person
			WHERE	PersonId = @KeyId


	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
