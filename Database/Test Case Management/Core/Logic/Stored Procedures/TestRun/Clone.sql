			IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestRunClone')
			BEGIN
				PRINT 'Dropping Procedure TestRunClone'
				DROP  Procedure TestRunClone
			END 
			GO

			PRINT 'Creating Procedure TestRunClone'
			GO

			/*********************************************************************************************
			**		File: 
			**		Name: TestRunClone
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
			*********************************************************************************************
			**		Change History
			*********************************************************************************************
			**		Date:		Author:				Description:
			**		--------	--------			------------------------------------------------------
			**		
			**********************************************************************************************/

			CREATE Procedure dbo.TestRunClone
			(
					@TestRunId			INT			= NULL 	OUTPUT	
				,	@ApplicationId			INT			= NULL	
				,	@Name					VARCHAR(50)						
				,	@Description			VARCHAR(100)						
				,	@SortOrder				INT								
				,	@AuditId				INT									
				,	@AuditDate				DATETIME	= NULL				
				,	@SystemEntityType		VARCHAR(50) = 'TestRun'			
			)
			AS
			BEGIN		

				SELECT	@ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
					,	@Description		= ISNULL(@Description, Description)
					,	@SortOrder			= ISNULL(@SortOrder, SortOrder)				
				FROM	dbo.TestRun
				WHERE	TestRunId		= @TestRunId

				EXEC dbo.TestRunInsert 
						@TestRunId		=	NULL
					,	@ApplicationId		=	@ApplicationId
					,	@Name				=	@Name
					,	@Description		=	@Description
					,	@SortOrder			=	@SortOrder
					,	@AuditId			=	@AuditId

				-- Create Audit Record
				EXEC dbo.AuditHistoryInsert
						@SystemEntityType		= @SystemEntityType
					,	@EntityKey				= @TestRunId
					,	@AuditAction			= 'Clone'
					,	@CreatedDate			= @AuditDate
					,	@CreatedByPersonId		= @AuditId	

			END	
			GO
