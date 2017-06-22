			IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestCaseOwnerClone')
			BEGIN
				PRINT 'Dropping Procedure TestCaseOwnerClone'
				DROP  Procedure TestCaseOwnerClone
			END 
			GO

			PRINT 'Creating Procedure TestCaseOwnerClone'
			GO

			/*********************************************************************************************
			**		File: 
			**		Name: TestCaseOwnerClone
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

			CREATE Procedure dbo.TestCaseOwnerClone
			(
					@TestCaseOwnerId			INT			= NULL 	OUTPUT	
				,	@ApplicationId			INT			= NULL	
				,	@Name					VARCHAR(50)						
				,	@Description			VARCHAR(100)						
				,	@SortOrder				INT								
				,	@AuditId				INT									
				,	@AuditDate				DATETIME	= NULL				
				,	@SystemEntityType		VARCHAR(50) = 'TestCaseOwner'			
			)
			AS
			BEGIN		

				SELECT	@ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
					,	@Description		= ISNULL(@Description, Description)
					,	@SortOrder			= ISNULL(@SortOrder, SortOrder)				
				FROM	dbo.TestCaseOwner
				WHERE	TestCaseOwnerId		= @TestCaseOwnerId

				EXEC dbo.TestCaseOwnerInsert 
						@TestCaseOwnerId		=	NULL
					,	@ApplicationId		=	@ApplicationId
					,	@Name				=	@Name
					,	@Description		=	@Description
					,	@SortOrder			=	@SortOrder
					,	@AuditId			=	@AuditId

				-- Create Audit Record
				EXEC dbo.AuditHistoryInsert
						@SystemEntityType		= @SystemEntityType
					,	@EntityKey				= @TestCaseOwnerId
					,	@AuditAction			= 'Clone'
					,	@CreatedDate			= @AuditDate
					,	@CreatedByPersonId		= @AuditId	

			END	
			GO
