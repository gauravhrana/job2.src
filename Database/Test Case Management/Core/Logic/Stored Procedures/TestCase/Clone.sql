			IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestCaseClone')
			BEGIN
				PRINT 'Dropping Procedure TestCaseClone'
				DROP  Procedure TestCaseClone
			END 
			GO

			PRINT 'Creating Procedure TestCaseClone'
			GO

			/*********************************************************************************************
			**		File: 
			**		Name: TestCaseClone
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

			CREATE Procedure dbo.TestCaseClone
			(
					@TestCaseId			INT			= NULL 	OUTPUT	
				,	@ApplicationId			INT			= NULL	
				,	@Name					VARCHAR(50)						
				,	@Description			VARCHAR(100)						
				,	@SortOrder				INT								
				,	@AuditId				INT									
				,	@AuditDate				DATETIME	= NULL				
				,	@SystemEntityType		VARCHAR(50) = 'TestCase'			
			)
			AS
			BEGIN		

				SELECT	@ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
					,	@Description		= ISNULL(@Description, Description)
					,	@SortOrder			= ISNULL(@SortOrder, SortOrder)				
				FROM	dbo.TestCase
				WHERE	TestCaseId		= @TestCaseId

				EXEC dbo.TestCaseInsert 
						@TestCaseId		=	NULL
					,	@ApplicationId		=	@ApplicationId
					,	@Name				=	@Name
					,	@Description		=	@Description
					,	@SortOrder			=	@SortOrder
					,	@AuditId			=	@AuditId

				-- Create Audit Record
				EXEC dbo.AuditHistoryInsert
						@SystemEntityType		= @SystemEntityType
					,	@EntityKey				= @TestCaseId
					,	@AuditAction			= 'Clone'
					,	@CreatedDate			= @AuditDate
					,	@CreatedByPersonId		= @AuditId	

			END	
			GO
