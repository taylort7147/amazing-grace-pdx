import { useState } from "react";
import { useForm, useFieldArray, Controller } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Box, CloseButton, Flex, List, Heading, HStack, VStack, Input, Fieldset, Separator } from "@chakra-ui/react";
import CopyableTextBoxController from "../../../components/CopyableTextBoxController";
import DateField from "../../../components/DateField";
import DurationInput from "../../../components/DurationInput";
import FormAddButton from "../../../components/FormAddButton";
import FormDeleteButton from "../../../components/FormDeleteButton";
import FormField from "../../../components/FormField";
import FormSaveButton from "../../../components/FormSaveButton";
import Scripture from "./Scripture"
import SeriesComboBox from "../../../components/SeriesComboBox";
import TextBox from "../../../components/TextBox";
import { messageSchema, notesSchema, audioSchema, videoSchema } from "@message-manager/shared/schemas/index.js"
import { parseBibleReference } from "../../../utils/bibleApiUtils.js"

function formatTitle(title) {
  return title ?? "";
}

function formatDescription(description) {
  return description ?? "";
}

function _StackSeparator() {
  return <Box height={50} />;
}

const EditBasicSettings = ({ form }) => {
  const { control, getValues, register } = form;
  const message = getValues();
  return (
    <Fieldset.Root size="md">

      {/* Heading */}
      <Fieldset.Legend>
        <Heading size="lg" mb={0}>Basic Information</Heading>
      </Fieldset.Legend>

      <Separator mt={2} />

      {/* Title */}
      <FormField mb={0} form={form} name="title" label="Title" required>
        <Input
          {...register("title")}
          defaultValue={formatTitle(message?.title)}
        />
      </FormField>

      {/* Description */}
      <FormField mb={0} form={form} name="description" label="Description">
        <TextBox
          {...register("description")}
          defaultValue={formatDescription(message?.description)}
        />
      </FormField>

      {/* Date */}
      <FormField mb={0} form={form} name="date" label="Date" help="Select the date" required>
        <Controller
          control={control}
          name="date"
          render={({ field }) => (
            <DateField
              value={field.value}
              onChange={(date) => field.onChange(date.toISOString())}
            />
          )}
          required
        />
      </FormField>

      {/* Series */}
      <FormField mb={0} form={form} name="seriesId" label="Series" required>
        <Controller
          control={control}
          name="seriesId"
          render={({ field }) => (
            <SeriesComboBox
              value={field.value}
              onValueChange={(e) => {
                const seriesId = (e.value?.length == 1) ? e.value[0] : null;
                field.onChange(seriesId);
              }}
            />
          )}
        />
      </FormField>
    </Fieldset.Root>
  );
};


const EditScripture = ({ form }) => {
  const { control, register } = form;
  const { fields, append, remove } = useFieldArray({
    control,
    name: "bibleReferences"
  });

  const [newReferences, setNewReferences] = useState("");

  const submitNewReferences = () => {
    parseBibleReference(newReferences).then(response => {
      // The result data is an array of parsed references
      const parsedReferences = response.data;
      parsedReferences.forEach(r => append(r));
      setNewReferences("");
    }).catch(err => {
      console.error("Error parsing Bible reference:", err);
    });
  };

  const handleKeyDown = (e) => {
    if (e.key === "Enter") {
      submitNewReferences(newReferences);
    }
  };

  return (
    <Fieldset.Root size="md">
      {/* Heading */}
      <Fieldset.Legend>
        <Heading size="lg" mb={0}>Scripture</Heading>
      </Fieldset.Legend>

      <Separator mt={2} />

      {/* Entries */}
      <List.Root>
        {fields.map((item, index) => (
          <List.Item key={item.id}>
            <HStack mb={1}>
              <Input hidden key={`${item.id}`} {...register(`bibleReferences.${index}.id`)} value={item.id} />
              <Scripture bibleReference={item} />
              <CloseButton onClick={() => remove(index)} />
            </HStack>
          </List.Item>
        ))}
        <List.Item>
          <HStack mb={1}>
            <Input
              value={newReferences}
              onChange={(e) => setNewReferences(e.target.value)}
              placeholder="Add one or more reference and press enter (e.g., John 3:16, Judges 6:11-16, 25-32)"
              onKeyDown={handleKeyDown}
            />
          </HStack>
        </List.Item>
      </List.Root>
    </Fieldset.Root >
  );
}

const EditNotes = ({ form }) => {
  const { getValues } = form;
  const notes = getValues()?.notes;
  return (
    <Fieldset.Root size="md">
      {/* Heading */}
      <HStack justify="space-between" align="center">
        <Fieldset.Legend>
          <Heading size="lg" mb={0}>Notes</Heading>
        </Fieldset.Legend>
        {notes === null
          ? <FormAddButton form={form} name="notes" schema={notesSchema} />
          : <FormDeleteButton form={form} name="notes" />}
      </HStack>

      <Separator mt={2} />

      {/* Fields */}
      {notes && <>
        <FormField mb={0} form={form} name="notes.url" label="URL" required>
          <CopyableTextBoxController form={form} name="notes.url" />
        </FormField>
      </>}
    </Fieldset.Root>);
};

const EditAudio = ({ form }) => {
  const { getValues } = form;
  const audio = getValues()?.audio;
  return (
    <Fieldset.Root size="md">
      {/* Heading */}
      <HStack justify="space-between" align="center" verticalAlign="bottom">
        <Fieldset.Legend>
          <Heading size="lg">Audio</Heading>
        </Fieldset.Legend>
        {audio === null
          ? <FormAddButton form={form} name="audio" schema={audioSchema} />
          : <FormDeleteButton form={form} name="audio" />}
      </HStack>

      <Separator mt={2} />

      {/* Fields */}
      {audio && <>
        {/* Download URL */}
        <FormField mb={0} form={form} name="audio.downloadUrl" label="Download URL" required>
          <CopyableTextBoxController form={form} name="audio.downloadUrl" />
        </FormField>

        {/* Stream URL */}
        <FormField mb={0} form={form} name="audio.streamUrl" label="Stream URL" required>
          <CopyableTextBoxController form={form} name="audio.streamUrl" />
        </FormField>
      </>}
    </Fieldset.Root >
  );
};

const EditVideo = ({ form }) => {
  const { control, getValues, register, formState: { errors } } = form;
  const video = getValues()?.video;
  return (
    <Fieldset.Root size="md">
      {/* Heading */}
      <HStack justify="space-between" align="center" mb={0}>
        <Fieldset.Legend>
          <Heading size="lg" mb={1}>Video</Heading>
        </Fieldset.Legend>
        {video === null
          ? <FormAddButton form={form} name="video" schema={videoSchema} />
          : <FormDeleteButton form={form} name="video" />}
      </HStack>

      <Separator mt={2} />

      {/* Fields */}
      {video && <>
        {/* YouTube Video ID */}
        <FormField mb={0} form={form} name="video.youTubeVideoId" label="YouTube Video ID" required>
          <Input
            {...register("video.youTubeVideoId")}
          />
        </FormField>

        {/* Start Time */}
        <FormField mb={0} form={form} name="video.messageStartTimeSeconds" label="Start Time" required>
          <Controller
            control={control}
            name="video.messageStartTimeSeconds"
            render={({ field }) => (
              <DurationInput
                value={field.value}
                onValueChange={(val) => field.onChange(val)}
              />
            )}
          />
        </FormField>
      </>}
    </Fieldset.Root >
  );
};

function MessageForm({ initialData, onSubmit }) {
  const form = useForm({
    resolver: zodResolver(messageSchema),
    defaultValues: initialData ?? {},
    mode: "onChange", // validate on each change
  });
  const { reset } = form;

  const handleSubmit = (data) => {
    onSubmit(data);
    reset(data); // New data becomes the default state
  };

  return (
    <form>
      <Heading size="2xl" mb={8}>Edit Message</Heading>
      <VStack gap={30} separator={<_StackSeparator />}>

        {/* Basic Information */}
        <EditBasicSettings form={form} />

        {/* Scripture */}
        <EditScripture form={form} />

        {/* Notes */}
        <EditNotes form={form} />

        {/* Audio */}
        <EditAudio form={form} />

        {/* Video */}
        <EditVideo form={form} />

        <Flex width="100%" justifyContent={"flex-end"}>
          <FormSaveButton form={form} onSubmit={handleSubmit} />
        </Flex>
      </VStack>
    </form>
  );
};

export default MessageForm;
