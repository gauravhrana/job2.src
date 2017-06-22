			IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationPublisherClone')
			BEGIN
				PRINT 'Dropping Procedure NotificationPublisherClone'
				DROP  Procedure NotificationPublisherClone
			END 
			GO

			PRINT 'Creating Procedure NotificationPublisherClone'
			GO

			/*********************************************************************************************
			**		File: 
			**		Name: NotificationPublisherClone
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

			CREATE Procedure dbo.NotificationPublisherClone
			(
					@NotificationPublisherId			INT			= NULL 	OUTPUT	
				,	@ApplicationId			INT			= NULL
				,	@Name					VARCHAR(50)						
				,	@Description			VARCHAR(100)						
				,	@SortOrder				INT								
				,	@AuditId				INT									
				,	@AuditDate				DATETIME	= NULL				
				,	@SystemEntityType		VARCHAR(50) = 'NotificationPublisher'			
			)
			AS
			BEGIN		

				SELECT	@ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
					,	@Description		= ISNULL(@Description, Description)
					,	@SortOrder			= ISNULL(@SortOrder, SortOrder)				
				FROM	dbo.NotificationPublisher
				WHERE	NotificationPublisherId		= @NotificationPublisherId

				EXEC dbo.NotificationPublisherInsert 
						@NotificationPublisherId		=	NULL
					,	@ApplicationId		=	@ApplicationId
					,	@Name				=	@Name
					,	@Description		=	@Description
					,	@SortOrder			=	@SortOrder
					,	@AuditId			=	@AuditId

				-- Create Audit Record
				EXEC dbo.AuditHistoryInsert
						@SystemEntityType		= @SystemEntityType
					,	@EntityKey				= @NotificationPublisherId
					,	@AuditAction			= 'Clone'
					,	@CreatedDate			= @AuditDate
					,	@CreatedByPersonId		= @AuditId	

			END	
			GO
